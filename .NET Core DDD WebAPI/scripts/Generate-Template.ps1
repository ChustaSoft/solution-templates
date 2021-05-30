param(
    [Parameter(Mandatory=$true)]
    [ValidateScript({
        # verify that the directory passed as argument contains the correct solution
        $solutionFileExists = Test-Path $_\ProjectTemplate.sln

        if ($solutionFileExists) {
            $true
        }
        else {
            Throw [System.Management.Automation.ItemNotFoundException] "Given directory does not contain the project on which the template is to be based."
        }
    })]
    [string]
    $ProjectDirectory,

    [Parameter(Mandatory=$true)]
    [string]
    $TemplateDirectory
)

# copy project to tmp directory
$TempDirectory = ".\tmp"
Remove-Item -Path $TempDirectory -Recurse -Force -ErrorAction SilentlyContinue
Copy-Item -Path $ProjectDirectory -Destination $TempDirectory -Recurse

# remove all "bin" and "obj" directories
$TempDirectory | Get-ChildItem -Filter bin -Directory -Recurse | Remove-Item -Recurse -Force
$TempDirectory | Get-ChildItem -Filter obj -Directory -Recurse | Remove-Item -Recurse -Force

# replace all mentions of "ProjectTemplate" with "$ext_safeprojectname$"
$TempDirectory | Get-ChildItem -File -Recurse | foreach { ($_ | Get-Content).Replace( "ProjectTemplate", '$ext_safeprojectname$') | Set-Content -Path $_.PsPath }

# remove solution file
Remove-Item -Path "$TempDirectory\ProjectTemplate.sln"

# remove current content of project template
$TemplateDirectory | Get-ChildItem -File -Recurse -Exclude *.vstemplate,__TemplateIcon.ico | Remove-Item

# copy content to template directory
$TempDirectory | Get-ChildItem | Copy-Item -Destination $TemplateDirectory -Force -Recurse

# cleanup
Remove-Item -Path $TempDirectory -Recurse -Force