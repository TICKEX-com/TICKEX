<<<<<<< HEAD
FROM openjdk:17 as buildstage
WORKDIR /app
COPY mvnw .
COPY .mvn .mvn
COPY pom.xml .
RUN ./mvnw dependency:go-offline
COPY src src
RUN ./mvnw package -Dmaven.test.skip=true
RUN mv target/*.jar service.jar

FROM openjdk:17
COPY --from=buildstage /app/service.jar .
ENTRYPOINT ["java", "-jar", "service.jar"]
=======
FROM maven:3.8.4-openjdk-17-slim as build
WORKDIR /app
COPY . .
RUN mvn clean install

FROM openjdk:17-oracle
VOLUME /tmp
COPY target/*.jar app.jar
ENTRYPOINT ["java", "-jar", "app.jar"]
>>>>>>> authentication
