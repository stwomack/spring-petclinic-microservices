function PublishDotNetApp
{
    param($ServiceName)
    
    Write-Host "---"
    Write-Host "Building Steeltoe $ServiceName service"
    dotnet publish -c release -o "steeltoe-petclinic-$ServiceName-service/target" "steeltoe-petclinic-$ServiceName-service/$((Get-Culture).TextInfo.ToTitleCase($ServiceName)).Api/Steeltoe.Petclinic.$ServiceName.Api.csproj"
}

function PublishJavaApp
{
    param($ServiceName)

    Write-Host "---"
    Write-Host "Building $ServiceName"
    ./mvnw clean package -pl "spring-petclinic-$ServiceName" -am
}

function ASCCreateApp {
    param ($AppName, $Runtime, $ExtraParams)
    
    Write-Host "---"
    Write-Host "Creating a home for $AppName with runtime $Runtime"
    Start-Process -FilePath "az" -ArgumentList "spring-cloud app create --name $AppName --runtime-version $Runtime $ExtraParams"
}

function ASCDeployDotNet {
    param ($ServiceName, [bool]$RunInForeground)

    Write-Host "---"
    ASCWaitForReady "$ServiceName-service"
    Write-Host "Deploying .NET service $ServiceName"
    $artifactPath = "steeltoe-petclinic-$ServiceName-service/$((Get-Culture).TextInfo.ToTitleCase($ServiceName)).Api/deploy.zip"
    if ($RunInForeground)
    {
        az spring-cloud app deploy --name $ServiceName-service --runtime-version NetCore_31 --main-entry $ServiceName-service.dll --artifact-path $artifactPath
    }
    else
    {
        Start-Process -FilePath "az" -ArgumentList "spring-cloud app deploy --name $ServiceName-service --runtime-version NetCore_31 --main-entry $ServiceName-service.dll --artifact-path $artifactPath"
    }
}

function ASCDeployJar {
    param($ServiceName, [bool]$RunInForeground)

    Write-Host "---"
    ASCWaitForReady "spring-$ServiceName"
    if ($RunInForeground)
    {
        az spring-cloud app deploy --name "spring-$ServiceName" --jar-path "spring-petclinic-$ServiceName/target/spring-petclinic-$ServiceName-2.3.2.jar" --verbose
    }
    else
    {
        Start-Process -FilePath "az" -ArgumentList "spring-cloud app deploy --name spring-$ServiceName --jar-path spring-petclinic-$ServiceName/target/spring-petclinic-$ServiceName-2.3.2.jar --verbose"
    }
}

function ASCWaitForReady{
    param($ServiceName)
    Write-Host "Waiting for $ServiceName to be ready..."
    $appstate = az spring-cloud app show --name $ServiceName | ConvertFrom-Json
    $status = $appstate.properties.provisioningState
    while ($status -ne "Succeeded")
    {
        Write-Host "Waiting for a successful provisioning status... Currently $status. Checking again in 5 seconds"
        Start-Sleep -s 5
        $appstate = az spring-cloud app show --name $ServiceName | ConvertFrom-Json
        $status = $appstate.properties.provisioningState
    }
    Write-Host "App Instance is ready for a deployment"
}
