# Udemy - Angular and Dotnet Ecommerce Application
 
### Steps to start the Web API and the client application.
- First, verify that you have Docker 🐋 and Docker Compose installed on your machine by running `docker -v` and `docker compose`.
- Additionally, you can also check for your dotnet, node, angular and npm versions by running their respective commands.
- Then run `docker compose up -d` and wait for the image to download and start `Redis Commander` 🔫 and `Redis` container to start.
- Once started, you can run `dotnet restore` and `dotnet watch start` to spin 🌌 up the server
- Then navigate inside the **client** folder by running `cd client`.
- Run `ng serve` to spin the server.

### Requirements. ✔
- Dotnet version 7
- Angular version 15
- Node version 16
- Npm version 9
- Docker with Docker Compose
- Stripe CLI @ latest

# Help with migrations
By default the project will be able to run migrations, however, if you want to create a new migration, following the steps will help you create migrations:

`cd Infrastructure`

`dotnet ef dbcontext list`

Copy the name of the DB Context that you want to use

Run command `dotnet ef add migrations "<your_migration_name>" -p Infrastructure -s API -c <copied_context_name>`

Now you will have the changeset of the models that you have made.
