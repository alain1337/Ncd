function Get-Directory {
    param
    (
        [string]$path,
        [string]$selector = ""
    )

    $out = (C:\Users\abart\OneDrive\Source\Ncd\bin\Debug\Ncd.exe $path $selector)
    if ($?) {
        Set-Location $out
    }
    else {
        Write-Output $out
    }
}

Export-ModuleMember -Function Get-Directory
Set-Alias -Name ncd -Value Get-Directory
Export-ModuleMember -Alias * -Function *