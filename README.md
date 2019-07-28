
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

**All types that do NOT have the '?' are after the type declaration are considered to not to be null-able.**

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

## Weird Details:

You might be thinking "This implementation will completely break my code base!" and you would be right, if they didn't make this functionality opt in.

Currently you have to specify if want these warnings to appear, there are two ways this can be done at the moment. 

Using Pragma Warnings: 

    #nullable enable
	... Code stuff
	#nullable disable / restore

Using CS.proj elements:

Add the following line to the first PropertyGroup element of the  projects csproj file: 

	<NullableContextOptions>enable</NullableContextOptions>






