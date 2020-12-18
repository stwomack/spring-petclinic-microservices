#!/bin/sh

export resourceGroupName="springpet-clinic"
export serviceInstanceName="asc-petclinic"
export stagingDeploymentName="petclinic-staging"
export configServerGitUri="https://github.com/steeltoeoss-incubator/spring-petclinic-microservices-config"

#Clean up everything - this takes quite a while if you're cleaning up everything
az group delete --name "${resourceGroupName}" --yes

#Provision ASC and set defaults
az extension add --name spring-cloud
az group create --location eastus --name "${resourceGroupName}"
az spring-cloud create --name "${serviceInstanceName}" -g "${resourceGroupName}"
az configure --defaults group="${resourceGroupName}"
az configure --defaults spring-cloud="${serviceInstanceName}"

echo "Setting up config server"
az spring-cloud config-server git set --name "${serviceInstanceName}" --uri "${configServerGitUri}" --label "azure-spring-cloud"

echo "Building admin server"
./mvnw clean package -pl "spring-petclinic-admin-server" -am

echo "Deploying spring admin server"
az spring-cloud app create --name "spring-admin-server" --runtime-version Java_8
az spring-cloud app deploy --name "spring-admin-server" --jar-path "spring-petclinic-admin-server/target/spring-petclinic-admin-server-2.3.2.jar" --verbose

echo "Building spring customers service"
# ./mvnw clean package -pl "spring-petclinic-customers-service" -am
dotnet publish -c Release -o "steeltoe-petclinic-customers-service/target" "steeltoe-petclinic-customers-service/Customers.Api/Steeltoe.Petclinic.Customers.Api.csproj"

echo "Deploying spring customers service"
# az spring-cloud app create --name "customers-service" --runtime-version Java_8
# az spring-cloud app deploy --name "customers-service" --jar-path "spring-petclinic-customers-service/target/spring-petclinic-customers-service-2.3.2.jar" --verbose
az spring-cloud app create --name "customers-service" --runtime-version NetCore_31
az spring-cloud app deploy --name "customers-service" --runtime-version NetCore_31 --main-entry "customers-service.dll" --artifact-path "steeltoe-petclinic-customers-service/Customers.Api/deploy.zip" --verbose

echo "Building spring vets service"
# ./mvnw clean package -pl "spring-petclinic-vets-service" -am
dotnet publish -c release -o "steeltoe-petclinic-vets-service/target" "steeltoe-petclinic-vets-service/Vets.Api/Steeltoe.Petclinic.Vets.Api.csproj"

echo "Deploying spring vets service"
# az spring-cloud app create --name "vets-service" --runtime-version Java_8
# az spring-cloud app deploy --name "vets-service" --jar-path "spring-petclinic-vets-service/target/spring-petclinic-vets-service-2.3.2.jar" --verbose
az spring-cloud app create --name "vets-service" --runtime-version NetCore_31
az spring-cloud app deploy --name "vets-service" --runtime-version NetCore_31 --main-entry "vets-service.dll" --artifact-path "steeltoe-petclinic-vets-service/Vets.Api/deploy.zip"

echo "Building spring visits service"
# ./mvnw clean package -pl "spring-petclinic-visits-service" -am
dotnet publish -c release -o "steeltoe-petclinic-visits-service/target" "steeltoe-petclinic-visits-service/Visits.Api/Steeltoe.Petclinic.Visits.Api.csproj"

echo "Deploying spring visits service"
# az spring-cloud app create --name "visits-service" --runtime-version Java_8
# az spring-cloud app deploy --name "visits-service" --jar-path "spring-petclinic-visits-service/target/spring-petclinic-visits-service-2.3.2.jar" --verbose
az spring-cloud app create --name "visits-service" --runtime-version NetCore_31
az spring-cloud app deploy --name "visits-service" --runtime-version NetCore_31 --main-entry "visits-service.dll" --artifact-path "steeltoe-petclinic-visits-service/Visits.Api/deploy.zip"

########### READY TO TURN IT ON ###########
echo "Building api gateway"
./mvnw clean package -pl "spring-petclinic-api-gateway" -am

echo "Deploying api gateway to default environment"
az spring-cloud app create --name "spring-api-gateway" --is-public --runtime-version Java_8
az spring-cloud app deploy --name "spring-api-gateway" --jar-path "spring-petclinic-api-gateway/target/spring-petclinic-api-gateway-2.3.2.jar"