
# PACMAN 

Almost classic pacman game , what has many options and configuration

## Getting Started

Application use DATABASE FIRST principle , that mean , first should execute .sql script.
After this application must work and you can play!
Other specific folder documents and etc. will automacy created.

## Information

Application can use custom .dll in runtime each .dll can have one or more behaviors of AI. They can be selected in 
```
Menu -> Options -> AI
```

Check AI settings you can at 
```
Menu -> Info 
```
 here you will see all active settings

Here information about 10 best players. It's loaded from Data Base
```
Menu -> Records
```

Change player name possible in **Menu -> Options**
Also here can change *map size* and *percentage of map bloks*

```
Menu -> Options -> Ghost Options
```
Here possible change ghost count by their type (red, green, blue) and their speed per type
**important** It's highest leve. Any custom AI speeds will rewrite by this param if it enable

Application logs have full information about game progress or any exceptions, for this used special type.
Logger write each action to file

### Used Technologies

* **.NET** 
* **WPF** 
* **LINQ** 
* **MS SQL** 

### Used Patterns 

* **MVVM** 
* **Singleton** 

### Most popular helping resources what was used

* [stackoverflow](https://stackoverflow.com/)  - about 55 - 60 %
* [MSDN](https://docs.microsoft.com/ru-ru/) - about 35 - 40 %


