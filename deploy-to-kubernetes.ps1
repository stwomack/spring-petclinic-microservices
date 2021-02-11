param (
    [String]$imagePrefix = "springcommunity",
    [switch]$skipBuildJava,
    [switch]$skipBuildDotNet,
    [String]$imageRegistry
)

$images = "spring-petclinic-api-gateway", "spring-petclinic-admin-server", "steeltoe-customers-service", "steeltoe-vets-service", "steeltoe-visits-service"
$version = "2.3.6"

if (!$skipBuildJava)
{
    Write-Host "Building images for Spring applications."
    ./mvnw spring-boot:build-image -pl spring-petclinic-admin-server,spring-petclinic-api-gateway
}

if (!$skipBuildDotNet)
{
    Write-Host "Building images for .NET applications."
    docker-compose build --parallel
}

if ($imagePrefix -eq "springcommunity")
{
    Write-Host "Image prefix wasn't set. The default value of 'springcommunity' will be used."
}
else
{
    Write-Host "Image prefix '$imagePrefix' will be used."
    foreach ($image in $images)
    {
        $tag = $image + ":" + $version
        docker tag "springcommunity/$tag" $imagePrefix/$tag
    }
}

if ($imageRegistry -ne $null)
{
    foreach ($image in $images)
    {
        $tag = "$imagePrefix/$image" + ":" + $version
        docker push $imageRegistry/$tag
    }
}

(Get-Content .\k8s\*.yaml -Raw) -replace "<ImagePrefix>", $imagePrefix | kubectl apply -f -