pack: 
	dotnet restore "src/netspec"
	dotnet pack "src/netspec" --output "artifacts/packages"
	dotnet restore "src/dotnet-test-netspec" -s "artifacts/packages"
	dotnet pack "src/dotnet-test-netspec" --output "artifacts/packages"

restore: pack
	dotnet restore -s "artifacts/packages"

build: restore
	dotnet build "src/netspec"
	dotnet build "src/dotnet-test-netspec"

tests:
	dotnet test "test/dotnet-test-netspec.test"

all: build tests