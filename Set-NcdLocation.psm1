function Set-NcdLocation {
    param
    (
        [string]$path,
        [string]$selector = ""
    )

    $out = (C:\Users\abart\OneDrive\Source\Ncd\bin\Debug\Ncd.exe $path $selector)
    if ($LastExitCode -eq 0) {
        Set-Location $out
    }
    else {
        Write-Output $out
    }
}

Export-ModuleMember -Function Set-NcdLocation
Set-Alias -Name ncd -Value Set-NcdLocation
Export-ModuleMember -Alias * -Function *