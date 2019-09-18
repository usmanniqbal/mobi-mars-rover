PROJECTPATH=./Mars-Rover/
PROJECTNAME=Mars-Rover.csproj
TESTPROJECTPATH=./Mars-Rover.Tests/

default: run
	
test:
	dotnet test ${TESTPROJECTPATH}

build: test
	dotnet build ${PROJECTPATH}${PROJECTNAME}

run: build
	export rovers=rovers
	dotnet run --project ${PROJECTPATH}${${PROJECTNAME}}
