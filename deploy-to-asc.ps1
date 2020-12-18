param (
    [String]$location = "eastus2",
    [String]$rg = "Steeltoe",
    [String]$ASCServiceInstance = "steeltoe",
    [switch]$initializeAzureSpringCloud,
    [switch]$buildJava,
    [switch]$buildDotNet,
    [switch]$provisionAppSlots,
    [switch]$deployAllApps,
    [switch]$deployJavaApps,
    [switch]$deployDotNetApps,
    [String]$configServerGitUri = "https://github.com/steeltoeoss-incubator/spring-petclinic-microservices-config"
)

$TotalTime = New-Object -TypeName System.Diagnostics.Stopwatch
$TotalTime.Start()

# include some helper functions that should make this file easier to read
. .\deployment-functions.ps1

if ($initializeAzureSpringCloud)
{
    az extension add --name spring-cloud
    az group create --location $location --name $rg
    az spring-cloud create --name $ASCServiceInstance -g $rg

    Write-Host "Setting up config server"
    az spring-cloud config-server git set --name "$ASCServiceInstance" --uri "$configServerGitUri" --label "azure-spring-cloud"
}

az configure --defaults group=$rg
az configure --defaults spring-cloud=$ASCServiceInstance

if ($provisionAppSlots)
{
    ASCCreateApp "spring-admin-server" "Java_8"
    # ASCCreateApp "customers-service" "Java_8"
    ASCCreateApp "customers-service" "NetCore_31"
    # ASCCreateApp "vets-service" "Java_8"
    ASCCreateApp "vets-service" "NetCore_31"
    # ASCCreateApp "visits-service" "Java_8"
    ASCCreateApp "visits-service" "NetCore_31"
    ASCCreateApp "spring-api-gateway" "Java_8" "--is-public"
}

if ($buildJava)
{
    #PublishJavaApp "customers-service"
    #PublishJavaApp "vets-service"
    #PublishJavaApp "visits-service"
    PublishJavaApp "api-gateway"
    PublishJavaApp "admin-server"
}

if ($buildDotNet)
{
    PublishDotNetApp "customers"
    PublishDotNetApp "vets"
    PublishDotNetApp "visits"
}

if ($deployDotNetApps -or $deployAllApps)
{
    ASCDeployDotNet "customers" $true
    ASCDeployDotNet "vets" $true
    ASCDeployDotNet "visits" $true
}

if ($deployJavaApps -or $deployAllApps)
{
    # ASCDeployJar "customers-service"
    # ASCDeployJar "vets-service"
    # ASCDeployJar "visits-service"
    Write-Host "Deploying spring admin server"
    ASCDeployJar "admin-server" $false

    Write-Host "Deploying api gateway to default environment"
    ASCDeployJar "api-gateway" $true
}

$TotalTime.Stop()
Write-Host "Total processing time:" $TotalTime.Elapsed.ToString()
