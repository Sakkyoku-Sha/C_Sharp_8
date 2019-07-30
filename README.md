
# C#  8.0 

### What is C# 8.0? and why should I care?


# How do I get this working? 

C# 8.0 will only be supported by versions of .NET Core 3.0 or greater. 

As of July 2019 IDE support for C# 8.0 is only available in the preview version builds of Visual Studio 2019. It can be found here : https://visualstudio.microsoft.com/vs/preview/. 

To compile C# 8.0 a version of the .NET framework 3.0 or greater is required, 3.0 can be found here : https://dotnet.microsoft.com/download/dotnet-core/3.0

# Everything is Nullable! 

This is pretty much the only reason anyone cares about C# 8.0,  so at least pay attention to this. 

This addition allows for code to be explicit in stating "what can and can't be null". consider the two following pieces of code.

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


## Ranges and Indices

C# 8.0 Adds native support for ranges and "end of sequence" access of sequences and arrays. 

The '^' operator is currently being defined as [^n] accesses the n element from the end of the array. ^1 is the last element of the array, ^2 is the second value from the end, ^0 is interestingly equivalent to Array.Length().

For example: 

    var arr = new int[] {1,2,3,4,5,6,7,8,9,10}; 
    var a = arr[^1]; // a = 10
    var b = arr[^2]; // b = 9
	var c = arr[^0]; // index out of bound exception.
	
Like Haskell or Prolog, C# 8.0 also include the ability to access/create sub arrays via ranges. 

    var subarr = arr[0..2] // subarr = {1,2}
	var subarr2 = arr[0..^1] // subarr2 = {1,2,3,4,5,6,7,8,9} 


# Small Additions

### Default Interface Members

This is a slightly contentious change. 

C# 8.0 allows you to define default behavior for Interface functions. Suppose you have an interface that is implemented by tons of classes, something like an IEntity interface. In C# 7.0, If we wanted to add a new method to IEntity everything which inherits from it would have to define that method. 

    public Interface IEntity
    {
	    string Name {get;}
	    EntityCategory {get;}
	     
	    ... 

	    //New Method
	    string ReserveType(){
		return Name + ":" + EntityCategory.ToString();
            } 	
    }

This allows us to extend interfaces to add new functionality without breaking any current functionality, but some argue that this addition could do more harm than good, as its implementation may lead to poor coding practices. 

### Using Declaration:

The Using Interface has been cleaned up in a way that will might make you actually use it now. 

It has been recommended for a long time that to properly manage memory in C#, the using method should be used. This requires the stated variable to implement the IDisposable interface. 

Before: 

    using(var file = new System.File(fileName)){
		...
	}

Now: 

    using var file = new System.File(fileName); 

<br>
You can use the "using" keyword before a type definition to specify that it will be disposed off with IDisposable.Dispose() at the end of the variables scope.

## Performance Enhancements:

### Static Local Functions: 

Local functions have been possible for sometime in C#, however the implmentation that is used has performanced costs with the call stack for the function to make the local variables accessible to the internal function. 

C# 8.0 adds the ability to have more data efficient local functions by making use of the static keyword.

	public void OutputData(){
		
		foreach(dataPoint in localData)
			printData(dataPoint);
		
		static void printData(Datapoint dp){
			//print data...
		}
	}

### Disposable Refstructs 

In the latest version of C# 7.0 for data efficiency purposes the 'ref struct' data structure was used which forced the structure to be stored in continous memory upon the call stack. One issue that these structs had was releasing their data from the call stack when the structs became too complex. 

In C# 8.0 all Refstructs implcitly implement the Dispoable interface so you can better define how the structs themselves are deconstructed. 

	ref struct dataPoint{
		IntPtr x, y;
		
		public void Dispose(){
			//
		}
	}

Which allows for using statements with these structs.
	
	using var data = new dataPoint(x,y) ...
	

# Asynchronous Streams

This change provides the capability to enumerate over asynchronous objects. 

This is done by two new public interfaces, IAsyncEnumerable\<out T> and IAsyncEnumerator\<out T>. 

So we can make a producer/consumer architecture using something similar to this: 

    public async IAsyncEnumerable<HttpData> HttpRequests(){
		while(m_running){
			yield return server.GetRequest();
		}
	}

At any point we could then call this HttpRequest as a Enumerable Type. To iterate through these data types 

    await foreach(var data in HttpRequests()){
		var response = ProcessHttp(data);
		SendResponse(response);
	} 

Thus you could define a field in an IServer Interface which would contain all HTTP request that will every occur while the server is running.

This addition is meant to provide support for the "pull" model of producer/consumer architecture. Which, as opposed to the "push" model, is better in scenarios where the producers are dealing with more data than the consumers are consuming. 

