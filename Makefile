TEST_PROJECTS = $(sort $(dir $(wildcard test/*/)))

build:
	dotnet restore "src/netspec"
	dotnet pack "src/netspec" --configuration Release --output "artifacts/packages"
	dotnet restore "src/dotnet-test-netspec" -s "artifacts/packages"
	dotnet pack "src/dotnet-test-netspec" --configuration Release --output "artifacts/packages"
	dotnet restore "test" -s "artifacts/packages"

tests:
	for dir in $(TEST_PROJECTS); do \
		cd $$dir; \
		dotnet build; \
		dotnet test; \
	done

all: build tests