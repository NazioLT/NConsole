# NConsole 1.0.0

NConsole is an in-game command console wich help you to easily debug your game.

### Features : 
* Display all Unity's logs.
* Enter Commands.
* Add your own commands.
* Autocompletion.
* Command history.
* Theme Customization.

### Commands : 
* Enum support
* Static methods
* Non-static methods
* Asynchronous methods
* Multiple execution types.

## Basic Usage : 

### In your code : 

* Import the **Nazio_LT.Tools.Console** namespace.
* Type [NCommand] above your method.
 
![ConsoleDemoStatic](https://github.com/NazioLT/NConsole/assets/94365544/5c9abfc6-e37d-4697-ba9d-1d9f846d982a)

### In Unity : 

* Go to your game, and create a console.

![ConsoleDemoImportConsole](https://github.com/NazioLT/NConsole/assets/94365544/f660bc92-edbc-402e-b9b1-19b8e403b126)

* Go in console and type your command Name.

![ConsoleDemoStatic2](https://github.com/NazioLT/NConsole/assets/94365544/ea20c416-eff6-4dc4-a58b-f267c8d03bf1)

![ConsoleDemoStatic3](https://github.com/NazioLT/NConsole/assets/94365544/6045a1e7-24a3-40f1-95ef-12e01928d474)

## Use on all script instances : 

### In your code : 

* Type [NCommand] above your method, and define the ExecutionMode to AllMonoBehaviourInstances.

![ConsoleDemoInstance](https://github.com/NazioLT/NConsole/assets/94365544/74e45561-ed68-49ae-87dc-d67965a21049)

### In Unity : 

* For the test, we're going to add 2 scripts in the scene. 

![ConsoleDemoInstance2](https://github.com/NazioLT/NConsole/assets/94365544/76ae9bac-65fd-49f1-a25d-6db6aa00adbb)

* The Console Execute the methods of all scripts.

![ConsoleDemoInstance3](https://github.com/NazioLT/NConsole/assets/94365544/53ce2c7e-9c95-48df-a486-b12614b3c7d4)

## Select a gameobject :

* Type in your terminal "Select Name".
* Your object is now selected.

## Use on one instance : 

### In your code : 

* Type [NCommand] above your method, and define the ExecutionMode to SelectedObjectInstance.

![ConsoleDemoSelectedObject](https://github.com/NazioLT/NConsole/assets/94365544/ffb5348d-57e4-4977-9be7-722b768f8d3e)

### In Unity : 

* For the demo, I'm going to define the _testText to "Instance Worked!" on the gameOjbect named "Test".

![ConsoleDemoSelectedObject2](https://github.com/NazioLT/NConsole/assets/94365544/0afd1a61-cc58-4239-8d61-c34a99a033fe)

* First, I select the object named "Test".
* Then, type the command, and it worked!

![ConsoleDemoSelectedObject3](https://github.com/NazioLT/NConsole/assets/94365544/3d59291b-4bf9-4805-953b-0221ecabb874)
