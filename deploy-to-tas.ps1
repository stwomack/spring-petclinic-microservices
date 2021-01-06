function WaitForService{
    param($ServiceName)
    Write-Host "Waiting for $ServiceName to be ready..."
    $status = (cf service $ServiceName | Out-String) -like "*create succeeded*"
    while (!$status)
    {
        Write-Host "Waiting for a successful provisioning status... Checking again in 5 seconds"
        Start-Sleep -s 5
        $status = (cf service $ServiceName | Out-String) -like "*create succeeded*"
    }
    Write-Host "$ServiceName is ready for use"
}

# use a dedicated space for this app
cf create-space petclinic
cf target -s petclinic

# Managed Spring Cloud Gateway doesn't support serving static files or aggregating calls like how this app was built
# We'll deploy a Jar of what's in this repo instead, see manifest.yml
#cf create-service p.gateway standard petclinicGateway

cf create-service p.config-server standard config-server -c '{\"git\": { \"uri\": \"https://github.com/steeltoeoss-incubator/spring-petclinic-microservices-config\" } }'
cf create-service p.service-registry standard service-registry

WaitForService "config-server"
WaitForService "service-registry"
.\mvnw package -pl spring-petclinic-api-gateway -DskipTests -P tas

cf push -f .\manifest.yml

# register services with the gateway
#WaitForService "petclinicGateway"
#cf bind-service customers-service petclinicGateway -c '{\"routes\": [{\"path\": \"/api/customer/**\"}]}'
#cf bind-service vets-service petclinicGateway -c '{\"routes\": [{\"path\": \"/api/vet/**\"}]}'
#cf bind-service visits-service petclinicGateway -c '{\"routes\": [{\"path\": \"/api/visit/**\"}]}'
# client-side app ??
# Push-Location spring-petclinic-api-gateway
# cf push -f .\manifest.yml
# Pop-Location