# Ncd
Comfortable NCD like directory changer for PowerShell

## Install

1. Import-Module Get-Directory.psm1
2. Set NCD_PATH environment variable

## Usage

Ncd allows you to use abreviated paths like this:

`ncd e\b\d`

This will search for a subdirectory starting with 'E' in the current directory and all directories in NCD_PATH. 
It then looks for a subdirectory starting with 'B' (like bin) in this and one starting with 'D' in that (like 'Debug').

If this is unique it will change to the directory (like ~\Src\Example\bin\Debug). If not a list of matches is printed. You can use an index to select it like this:

`ncd e\b\d 2`
