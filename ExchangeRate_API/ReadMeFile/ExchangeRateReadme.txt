Running The Exchange Rate API 

1. You need to Install Docker
2. you need redismanager installed and set the host to 6379
3. rub this command on powersell after installing docker pls ignore the quote 'docker run -p 6379:6379 --name redis-master -e REDIS_REPLICATION_MODE=master -e ALLOW_EMPTY_PASSWORD=yes bitnami/redis:latest'
4. check the appsetings to change the redisSettings:enabled to false to use app without redis while to true to use redis 
5. change the connection string in the appsettings.json to desired connection string 
6. check the database for the table 
7. register a user using the register user endpoint 
8. login using login end point 
9. use the bearer token in the swagger docs to access the other endpoints 
