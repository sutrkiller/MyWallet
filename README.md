# MyWallet #
=============
Expense manager web application. 

Part of a project for the course PV247 - Agile Web Project in .NET at Faculty of Informatics, Masaryk University, Brno - Czech Republic.

## Initial Class Diagram: ##

![alt tag](https://raw.githubusercontent.com/sutrkiller/MyWallet/master/class-diagram.png)


## Coding Conventions ##

#### Naming Conventions ####

***private fields*** in lowerCamelCase starting with underscore  
```c#
private string _privateField;
```

***public properties*** in UpperCamelCase (with default get/set on one line, on more lines otherwise)  
```c#
public string PublicProperty { get; set; }
```

***methods*** in UpperCamelCase  
```c# 
private string PrivateMethod(){} //curly brackets always at its own line 
```

#### Formatting Conventions ####

***if/else/switch*** always with curly brackets  
```c# 
if ( ... )
{
  ...  
}
```  

