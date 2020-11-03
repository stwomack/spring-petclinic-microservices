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
az spring-cloud create -n "${serviceInstanceName}" -g "${resourceGroupName}"
az configure --defaults group="${resourceGroupName}"
az configure --defaults spring-cloud="${serviceInstanceName}"

echo "Setting up config server"
az spring-cloud config-server git set -n "${serviceInstanceName}" --uri "${configServerGitUri}" --label "azure-spring-cloud"

echo "Building admin server"
./mvnw clean package -pl "spring-petclinic-admin-server" -am

echo "Deploying admin server to default environment"
az spring-cloud app create --name "spring-admin-server" --is-public --runtime-version Java_8
az spring-cloud app deploy -n "spring-admin-server" --jar-path "spring-petclinic-admin-server/target/spring-petclinic-admin-server-2.3.2.jar" --verbose
#az spring-cloud app logs -n "spring-admin-server"

# echo "Building spring customers service"
# ./mvnw clean package -pl "spring-petclinic-customers-service" -am

# echo "Deploying spring customers service"
# az spring-cloud app create --name "customers-service" --is-public --runtime-version Java_8
# az spring-cloud app deploy -n "customers-service" --jar-path "spring-petclinic-customers-service/target/spring-petclinic-customers-service-2.3.2.jar" --verbose

echo "Building steeltoe customers service"
dotnet publish -c release -o steeltoe-petclinic-customers-service/target steeltoe-petclinic-customers-service/src/main/steeltoe-petclinic-customers-api.csproj

echo "Deploying steeltoe customers service"
az spring-cloud app create --name "customers-service" --is-public --runtime-version NetCore_31
az spring-cloud app deploy -n "customers-service" -g "${resourceGroupName}" -s "${serviceInstanceName}" --runtime-version NetCore_31 --main-entry customers-service.dll --artifact-path steeltoe-petclinic-customers-service/src/main/deploy.zip

# echo "Building spring vets service"
# ./mvnw clean package -pl "spring-petclinic-vets-service" -am

# echo "Deploying spring vets service"
# az spring-cloud app create --name "vets-service" --is-public --runtime-version Java_8
# az spring-cloud app deploy -n "vets-service" --jar-path "spring-petclinic-vets-service/target/spring-petclinic-vets-service-2.3.2.jar" --verbose

echo "Building steeltoe vets service"
dotnet publish -c release -o steeltoe-petclinic-vets-service/target steeltoe-petclinic-vets-service/src/main/steeltoe-petclinic-vets-api.csproj

echo "Deploying steeltoe vets service"
az spring-cloud app create --name "vets-service" --is-public --runtime-version NetCore_31
az spring-cloud app deploy -n "vets-service" -g "${resourceGroupName}" -s "${serviceInstanceName}" --runtime-version NetCore_31 --main-entry vets-service.dll --artifact-path steeltoe-petclinic-vets-service/src/main/deploy.zip

# echo "Building spring visits service"
# ./mvnw clean package -pl "spring-petclinic-visits-service" -am

# echo "Deploying spring visits service"
# az spring-cloud app create --name "visits-service" --is-public --runtime-version Java_8
# az spring-cloud app deploy -n "visits-service" --jar-path "spring-petclinic-visits-service/target/spring-petclinic-visits-service-2.3.2.jar" --verbose

echo "Building steeltoe visits service"
dotnet publish -c release -o steeltoe-petclinic-visits-service/target steeltoe-petclinic-visits-service/src/main/steeltoe-petclinic-visits-api.csproj

echo "Deploying steeltoe visits service"
az spring-cloud app create --name "visits-service" --is-public --runtime-version NetCore_31
az spring-cloud app deploy -n "visits-service" -g "${resourceGroupName}" -s "${serviceInstanceName}" --runtime-version NetCore_31 --main-entry visits-service.dll --artifact-path steeltoe-petclinic-visits-service/src/main/deploy.zip


########### READY TO TURN IT ON ###########
echo "Building api gateway"
./mvnw clean package -pl "spring-petclinic-api-gateway" -am

echo "Deploying api gateway to default environment"
az spring-cloud app create --name "spring-api-gateway" --is-public --runtime-version Java_8
az spring-cloud app deploy -n "spring-api-gateway" --jar-path "spring-petclinic-api-gateway/target/spring-petclinic-api-gateway-2.3.2.jar"