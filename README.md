
# C#  8.0 

### What is C# 8.0? and why should I care?


# How do I get this working? 

C# 8.0 will only be supported by versions of .NET Core 3.0 or greater. 

As of July 2019 IDE support for C# 8.0 is only available in the preview version builds of Visual Studio 2019. It can be found here : https://visualstudio.microsoft.com/vs/preview/. 

To compile C# 8.0 a version of the .NET framework 3.0 or greater is required, 3.0 can be found here : https://dotnet.microsoft.com/download/dotnet-core/3.0

# Everything is Nullable! 

This is pretty much the only reason anyone cares about C# 8.0,  so at least pay attention to this. 

This addition allows for code to be explicit in stating "what can and can't be null" consider the two following pieces of code.

	public interface INullableClient{
		IServer? ServerConnection;
		...
		
---
    
    public interface IClient{
		IServer ServerConnection;
		...

All Reference types can be specified to be Nullable with the use of '?' following the type name.

**All types that do NOT have the '?' symbol after the type declaration are considered to not to be null-able.**

This allows the code to directly express the requirement difference between the classes: 

> All Clients ***can*** have a connection to a server.

> All Clients ***must*** have a connection to a server

This is enforced through compile time build warnings. For example: 
	
	IServer ServerConnection {get; set;}	

	public void set(ServerConnection? connection){
		ServerConnection = connection; //warning: Possible null reference exception/ 
	}
    

<br>
 These warnings provide powerful tool to enforce the null object pattern. If properly instantiated it should not be possible to have a null reference exception without some sort of build warning notifying a developer that it is unhanded. 

### Weird Details:

You might be thinking "This implementation will completely break my code base!" and you would be right, if they didn't make this functionality opt in.

Currently you have to specify if want these warnings to appear, there are two ways this can be done:

*Using Pragma Warnings:* 

    #nullable enable
    
	... Code 
	
	#nullable disable / restore

*Using CS.proj elements:*

Add the following line to the first PropertyGroup element of the  projects csproj file: 

	<NullableContextOptions>enable</NullableContextOptions>

# Better pattern matching

Expanding on C# 7 pattern features 8.0 introduces a few fairly interesting additions to switch statements.

### Tuple Matching: 

Switches now support pattern matching based on tuples. 


	public ServerState ServerState {get; set;}
	
	public Access GetAccess(ClientType type){
		
		switch(ServerState, type){
			case(ServerState.Accepting, _)
				return Access.Allow;		
			case(Server.State.Busy, ClientType.Admin): 
				return Access.Allow;
			case(Server.State.Busy, ClientType.Standard): 
				return Access.Deny;
			...
			default:
				return Access.Deny
		}
	}

This is particular useful in state and transition based architecture, such as a decision tree. It makes this type of code ***far*** easier to read and understanding whilst removing many levels of required functional nesting.

### Switch Expressions:

The *switch* keyword can now also be used after a type definition for the switch statement to be used as an expression. Which unlike a statement returns a value.

We could express the above switch statement as a switch expression:


	return (ServerState, type) switch {
		(ServerState.Accepting, _) => Access.Allow, 
		(ServerState.Busy, ClientType.Admin) => Access.Allow,		
		(ServerState.Busy, ClientType.Standard) => Access.Deny,
		_ => Access.Deny
	};		
	
Syntax Differences: 

- The 'case' keyword is not needed.
- The '=>' symbol is used to specify the expressions return value.
- Commas at the end of each case functionally replace the 'break' keyword.
- 'default' key word is not used.


### Property Patterns: 

This is a very simple change that makes conditional statements a bit more readable, it adds no functionality however. 
  
     public void ConnectClient(IClient client)
     {
        if ((client is IClient {ClientType: ClientType.Admin})) 
            AdminConnect();
        else
            StandardConnect()       
     }
	
You can specify an object of a type with public fields equal to certain objects. This makes conditional operations on large amount of field equivalence statements easier to read.




