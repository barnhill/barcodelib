#!/bin/bash

msbuild /p:Configuration=Release /p:IncludeSymbols=true /p:SymbolPackageFormat=snupkg
nuget pack -Symbols -SymbolPackageFormat snupkg -Properties Configuration=Release;DoNotInvokeNugetPackDirectly= 
