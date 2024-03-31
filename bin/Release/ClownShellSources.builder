SOURCES_FILE=/home/mel/Documents/csharp/ClownShellSources/ClownShell.sources;



#=$PATH=/home/mel/Documents;
#=PRINT($PATH)=;
#=exiting the program;
#=EXIT()=;
SYS_CALL(bash)=/home/mel/Documents/csharp/ClownShell/./commit.sh;

EXIT()=;
#=Removing old Packages;
FUNCTION=DELETE;
NAME=ClownShell;
SLEEP(5000)=;

FUNCTION=DELETE;
NAME=QuickTools;
SLEEP(5000)=;

#=Testing to add just some other files out side of the bin folder;
NAME=TEST;
BIN_PATH=;



#=QuickTools;
NAME=QuickTools;
BIN_PATH=/home/mel/Documents/csharp/QuickTools/bin;
SOURCE_URL=https://github.com/Mel4221/QuickTools/tree/testing;
CREATOR=MBV;
DESCRIPTION=This is my first C# Libreary and this has been created in the C# languague to try to help reduce the amout of code required to create simple tools;
FUNCTION=ADD;


#=ClownShell;
NAME=ClownShell;
BIN_PATH=/home/mel/Documents/csharp/ClownShell/bin;
SOURCE_URL=https://github.com/Mel4221/ClownShell/tree/v6;
CREATOR=MBV;
DESCRIPTION=This is a Command Line Inteface for the library QuickTools which calls all the methods insde of it and combine it as a scripting language;
FUNCTION=ADD;

PRINT-ALL()=;












#=VAR=GIT
#=VAR=ARGS;
#=GIT=git;
#=ARGS=add -A {BIN_PATH(../../)};
#=SYS_ARG=ARGS;
#=SYS_CALL=GIT;
#=ARGS=commit {BIN_PATH(../../)} -C update;
#=SYS_ARG=ARGS;
#=SYS_CALL=git;
#=ARGS=push {BIN_PATH(../../)};
#=SYS_CALL=GIT;
