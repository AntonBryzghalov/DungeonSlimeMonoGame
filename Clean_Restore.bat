dotnet clean

rd /s /q Game.Core\bin
rd /s /q Game.Core\obj
rd /s /q Game.Android\bin
rd /s /q Game.Android\obj
rd /s /q MonoGameLibrary\bin
rd /s /q MonoGameLibrary\obj

dotnet restore