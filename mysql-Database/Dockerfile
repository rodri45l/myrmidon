FROM mysql:latest

# environment variables for root user password, database name and user
ENV MYSQL_ROOT_PASSWORD=mmyrmidontest
ENV MYSQL_DATABASE=myrmidon
ENV MYSQL_USER=myrmidon_admin
ENV MYSQL_PASSWORD=MyrmidonProject
ENV SQL_FILE_PATH=/docker-entrypoint-initdb.d/testdata.sql

# create data directory and bind mount as volume for data persistence
RUN mkdir -p /var/lib/mysql
VOLUME /var/lib/mysql

# Expose the MySQL port
EXPOSE 3306


# COPY testdata.sql $SQL_FILE_PATH

EXPOSE 3306
CMD ["mysqld"]
# Run mysqld as the entrypoint and command
# ENTRYPOINT ["sh", "-c", "mysqld & sleep 20s; mysql -u root -p${MYSQL_ROOT_PASSWORD} ${MYSQL_DATABASE} < ${SQL_FILE_PATH}"]
