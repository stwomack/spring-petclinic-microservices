Write-Host 'Deleting all apps and dedicated space...'
cf delete visits-service -f
cf delete customers-service -f
cf delete vets-service -f
cf delete api-gateway -f
cf delete-service config-server -f
cf delete-service service-registry -f
cf delete-space petclinic -f