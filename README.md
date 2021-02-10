# Distributed version of the Spring PetClinic Sample Application built with Spring Cloud and Steeltoe

[![License](https://img.shields.io/badge/License-Apache%202.0-blue.svg)](https://opensource.org/licenses/Apache-2.0)

This project is a fork of the [microservices version of PetClinic](https://github.com/spring-petclinic/spring-petclinic-microservices), built to demonstrate how Steeltoe and Spring can be used together to build polyglot applications.
In addition to Spring Cloud Gateway, Spring Cloud Circuit Breaker, Spring Cloud Config, Spring Cloud Sleuth, Resilience4j, Micrometer, and the Eureka Service Discovery from the [Spring Cloud Netflix](https://github.com/spring-cloud/spring-cloud-netflix) technology stack, this fork adds versions of the service applications built with .NET and Steeltoe.

* This branch exists to focus on [Azure Spring Cloud](#run-pet-clinic-on-azure-spring-cloud)

## Starting services locally with docker-compose

In order to start entire infrastructure using Docker, images for the components build with Spring must be built by executing `./mvnw spring-boot:build-image` from a project root. Images for the .NET variants can be built automatically.
Once those images are ready, you can start them with a single command `docker-compose up`. Containers are expected to fail and restart until the config server and discovery server are both up and running.
After starting services it takes a while for API Gateway to be in sync with service registry, so don't be scared of initial Spring Cloud Gateway timeouts.
You can track services availability using Eureka dashboard available by default at <http://localhost:8761>.

The `master` branch uses an  Alpine linux  with JRE 8 as Docker base. You will find a Java 11 version in the `release/java11` branch.

*NOTE: Under MacOSX or Windows, make sure that the Docker VM has enough memory to run the microservices. The default settings
are usually not enough and make the `docker-compose up` painfully slow.*

## Starting services locally with Project Tye

[Project Tye](https://github.com/dotnet/tye) is a tool from Microsoft that makes developing, testing, and deploying microservices and distributed applications easier. While it understands .NET applications very well, it is not built to run Java applications directly, so all of the non-.NET components in the Pet Clinic need to be run as Docker images. As with the docker-compose approach, you must first build the images by executing `./mvnw spring-boot:build-image` from the project root.

Once images have been built for the Java components, start everything with the command `tye run --docker`. Use the [Tye dashboard](http://localhost:8000) to track the progress of the components as they start up. It is expected for various components to restart several times until the config server is ready for requests, and it may take some additional time after that before the gateway is able to reach the backing services while it syncs with the service registry.

## Starting services locally without Docker

Every Java-based microservice is a Spring Boot application and can be started locally using IDE ([Lombok](https://projectlombok.org/) plugin has to be set up) or `./mvnw spring-boot:run` command. Please note that supporting services (Config and Discovery Server) must be started before any other application (Customers, Vets, Visits and API).
Startup of Tracing server, Admin server, Grafana and Prometheus is optional.
If everything goes well, you can access the following services at given location:

* Discovery Server - <http://localhost:8761>
* Config Server - <http://localhost:8888>
* AngularJS frontend (API Gateway) - <http://localhost:8080>
* Customers, Vets and Visits Services - random port, check Eureka Dashboard
* Tracing Server (Zipkin) - <http://localhost:9411/zipkin/> (we use [openzipkin](https://github.com/openzipkin/zipkin/tree/master/zipkin-server))
* Admin Server (Spring Boot Admin) - <http://localhost:9090>
* Grafana Dashboards - <http://localhost:3000>
* Prometheus - <http://localhost:9091>

You can tell Config Server to use your local Git repository by using `native` Spring profile and setting
`GIT_REPO` environment variable, for example:
`-Dspring.profiles.active=native -DGIT_REPO=/projects/spring-petclinic-microservices-config`

## Run Pet Clinic on Azure Spring Cloud

Azure Spring Cloud provides fully managed Eureka, Config Server, and Zipkin-compatible services that require only minimal setup work. We still need to publish/compile and deploy the gateway, services and (optionally) Spring Boot Admin. Scripts are provided in both [bash](./deploy-to-asc.sh) and [Powershell](./deploy-to-asc.ps1) that can allocate a new Azure Spring Cloud instance and perform all of the tasks needed to deploy this sample, assuming .NET and Java SDKs and the Azure CLI have all been installed.

Please review the contents of the scripts before executing! Each of the deployment scripts _can_ do the following but may differ in _how_:

* Ensure Azure Spring-Cloud az cli extension is installed
* Create a resource group
* Provision an Azure Spring Cloud Instance
* Point the built-in Config Server to [the config repo](https://github.com/steeltoeoss-incubator/spring-petclinic-microservices-config)
* Provision a slot for each application
  * Admin Server and Api Gateway both use `--is-public` to enable access from outside the private network
* Compile applications
* Publish applications
* Deploy applications

To clean up resources when you're done, the simplest approach is to delete the resource group created by the script.

## Understanding the Spring Petclinic application

[See the presentation of the Spring Petclinic Framework version](http://fr.slideshare.net/AntoineRey/spring-framework-petclinic-sample-application)

[A blog post introducing the Spring Petclinic Microservices](http://javaetmoi.com/2018/10/architecture-microservices-avec-spring-cloud/) (french language)

You can then access petclinic here: <http://localhost:8080/>

![Spring Petclinic Microservices screenshot](docs/application-screenshot.png)

## Architecture diagram of the Spring Petclinic Microservices

![Spring Petclinic Microservices architecture](docs/revised-architecture-diagram.png)

## In case you find a bug/suggested improvement for Spring Petclinic Microservices

Our issue tracker is available here: <https://github.com/spring-petclinic/spring-petclinic-microservices/issues>

## Database configuration

In its default configuration, Petclinic uses an in-memory database (HSQLDB) which gets populated at startup with data.
A similar setup is provided for MySql in case a persistent database configuration is needed.
Dependency for Connector/J, the MySQL JDBC driver is already included in the `pom.xml` files.

### Start a MySql database

You may start a MySql database with docker:

```bash
docker run -e MYSQL_ROOT_PASSWORD=petclinic -e MYSQL_DATABASE=petclinic -p 3306:3306 mysql:5.7.8
```

or download and install the MySQL database (e.g., MySQL Community Server 5.7 GA), which can be found here: https://dev.mysql.com/downloads/

### Use the Spring 'mysql' profile

To use a MySQL database, you have to start 3 microservices (`visits-service`, `customers-service` and `vets-services`)
with the `mysql` Spring profile. Add the `--spring.profiles.active=mysql` as program argument.

By default, at startup, database schema will be created and data will be populated.
You may also manually create the PetClinic database and data by executing the `"db/mysql/{schema,data}.sql"` scripts of each 3 microservices.
In the `application.yml` of the [Configuration repository], set the `initialization-mode` to `never`.

If you are running the microservices with Docker, you have to add the `mysql` profile into the [Dockerfile](docker/Dockerfile):

```bash
ENV SPRING_PROFILES_ACTIVE docker,mysql
```

In the `mysql section` of the `application.yml` from the [Configuration repository], you have to change
the host and port of your MySQL JDBC connection string.

## Custom metrics monitoring

Grafana and Prometheus are included in the `docker-compose.yml` configuration, and the public facing applications
have been instrumented with [MicroMeter](https://micrometer.io) to collect JVM and custom business metrics.

A JMeter load testing script is available to stress the application and generate metrics: [petclinic_test_plan.jmx](spring-petclinic-api-gateway/src/test/jmeter/petclinic_test_plan.jmx)

![Grafana metrics dashboard](docs/grafana-custom-metrics-dashboard.png)

### Using Prometheus

* Prometheus can be accessed from your local machine at <http://localhost:9091>

### Using Grafana with Prometheus

* An anonymous access and a Prometheus datasource are setup.
* A `Spring Petclinic Metrics` Dashboard is available at the URL <http://localhost:3000/d/69JXeR0iw/spring-petclinic-metrics>.
You will find the JSON configuration file [here](docker/grafana/dashboards/grafana-petclinic-dashboard.json).
* You may create your own dashboard or import the [Micrometer/SpringBoot dashboard](https://grafana.com/dashboards/4701) via the Import Dashboard menu item.
The id for this dashboard is `4701`.

### Custom metrics

Spring Boot registers a lot number of core metrics: JVM, CPU, Tomcat, Logback...
The Spring Boot auto-configuration enables the instrumentation of requests handled by Spring MVC.
All those three REST controllers `OwnerResource`, `PetResource` and `VisitResource` have been instrumented by the `@Timed` Micrometer annotation at class level.

* `customers-service` application has the following custom metrics enabled:
  * @Timed: `petclinic.owner`
  * @Timed: `petclinic.pet`
* `visits-service` application has the following custom metrics enabled:
  * @Timed: `petclinic.visit`

## Looking for something in particular?

| Spring Cloud components         | Resources  |
|---------------------------------|------------|
| Configuration server            | [Config server properties](spring-petclinic-config-server/src/main/resources/application.yml) and [Configuration repository] |
| Service Discovery               | [Eureka server](spring-petclinic-discovery-server) and [Service discovery client](spring-petclinic-vets-service/src/main/java/org/springframework/samples/petclinic/vets/VetsServiceApplication.java) |
| API Gateway                     | [Spring Cloud Gateway starter](spring-petclinic-api-gateway/pom.xml) and [Routing configuration](/spring-petclinic-api-gateway/src/main/resources/application.yml) |
| Docker Compose                  | [Spring Boot with Docker guide](https://spring.io/guides/gs/spring-boot-docker/) and [docker-compose file](docker-compose.yml) |
| Circuit Breaker                 | [Resilience4j fallback method](spring-petclinic-api-gateway/src/main/java/org/springframework/samples/petclinic/api/boundary/web/ApiGatewayController.java)  |
| Grafana / Prometheus Monitoring | [Micrometer implementation](https://micrometer.io/), [Spring Boot Actuator Production Ready Metrics] |

 Front-end module  | Files |
|-------------------|-------|
| Node and NPM      | [The frontend-maven-plugin plugin downloads/installs Node and NPM locally then runs Bower and Gulp](spring-petclinic-ui/pom.xml)  |
| Bower             | [JavaScript libraries are defined by the manifest file bower.json](spring-petclinic-ui/bower.json)  |
| Gulp              | [Tasks automated by Gulp: minify CSS and JS, generate CSS from LESS, copy other static resources](spring-petclinic-ui/gulpfile.js)  |
| Angular JS        | [app.js, controllers and templates](spring-petclinic-ui/src/scripts/)  |

## Interesting Spring Petclinic forks

The Spring Petclinic `main` branch in the main [spring-projects](https://github.com/spring-projects/spring-petclinic)
GitHub org is the "canonical" implementation, currently based on Spring Boot and Thymeleaf.

This [spring-petclinic-microservices](https://github.com/spring-petclinic/spring-petclinic-microservices/) project is one of the [several forks](https://spring-petclinic.github.io/docs/forks.html)
hosted in a special GitHub org: [spring-petclinic](https://github.com/spring-petclinic).
If you have a special interest in a different technology stack
that could be used to implement the Pet Clinic then please join the community there.

## Contributing

The [issue tracker](https://github.com/spring-petclinic/spring-petclinic-microservices/issues) is the preferred channel for bug reports, features requests and submitting pull requests.

For pull requests, editor preferences are available in the [editor config](.editorconfig) for easy use in common text editors. Read more and download plugins at <http://editorconfig.org>.

[Configuration repository]: https://github.com/spring-petclinic/spring-petclinic-microservices-config
[Spring Boot Actuator Production Ready Metrics]: https://docs.spring.io/spring-boot/docs/current/reference/html/production-ready-metrics.html
