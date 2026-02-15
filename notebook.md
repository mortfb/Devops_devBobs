# Devbobs's Notebook 
**Team:** Devbobs  
**Contributors:** Marius, Morten, Jonas & Laura

---

#### Format
Header with Lecture number.

---

#### What should be written down
- Keep a record of **when** you did **what**. 
- Note down **what went wrong**, **where** you found a **solution**, and **keep links** for that.


# Lecture 02
03/02 | 10:01: Created notebook.md <br>
03/02 | 10:10: Created project board for issues.<br>
- Issues with board not being public - fixed.

03/02 | 11:40: Imported and refactored Chirp to replace minitwit
- Copied chirp project files
- Refactored to remove OAuth
- Can be run with `dotnet run`.
- Confused about how we should handle tests. Asked TA - we need to choose what we want. Decided to cut and paste in current minitwit_tests.py file to make it work with Chirp.

03/02 | 22:50: Created dockerfile to containerize Chirp / Minitwit. 
- created _dockerfile_ branch.
- Looked at guides: https://medium.com/@aliyildizoz/understanding-asp-net-core-dockerfile-a523233bb9a4 & https://learn.microsoft.com/en-us/dotnet/core/docker/build-container?tabs=linux&pivots=dotnet-8-0 & https://hub.docker.com/r/microsoft/dotnet-runtime
- Had problems copying files, looked at https://stackoverflow.com/questions/74120448/understanding-this-docker-file-where-are-the-files-being-copied
- create docker image with `docker build -t marho/mychirp .` This had an issue with running forever.

07/02 | 13:00: Fixed issue with docker file running forever
- Found it was because the dockerfile was running dotnet run, which ofc makes it run and not build it. Then looked at https://stackoverflow.com/questions/74382131/docker-net-the-command-could-not-be-loaded-while-dotnet-and-dll-file-are-pre which showed example of how to build, publish and then create an ENTRYPOINT.
- The image can now be created. Can see with `docker image ls`, and run / create with container with `docker run --rm marho/mychirp`
- Ran into issue when running server. It opened on `Now listening on: http://[::]:8080`. Fix: Needed to define `-p 8080:8080` in the command when running and `EXPOSE 8080` in the Dockerfile - like the exercises from this week.
- Can now run start the container with `docker run -p 8080:8080 marho/mychirp` and open `http://localhost:8080/`. Added my notes to the commit.

# Lecture 03
10/02: 11:35: Refactord Chirp to be named MiniTwit & Moved legacy code to own branch.

10/02  14:50: Added the OpenAPI simulator.
- Started by copying the .json API into our repo root
        `cp ~/<path_to_lecture_notes>/sessions/session_03/API_Spec/swagger3.json`

- The ran this command in the same directory
    `docker run --rm -v "$(pwd):/local" openapitools/openapi-generator-cli:v7.19.0 \
  generate \
  -i /local/swagger3.json \
  -g aspnetcore \
  -o /local/out/itu-minitwit-sim-stub \
  --additional-properties=buildTarget=program,aspnetCoreVersion=8.0,operationIsAsync=true,nullableReferenceTypes=true,useSwashbuckle=true`
    - Read about what the above command does here: https://github.com/itu-devops/BSc_lecture_notes/blob/master/sessions/session_03/API_Spec/README.md

    - After running this an /out directory appeared. 
    - In the Org.OpenAPITools folder a new dokcer file is located.
    - Move this docker file out into the itu-minitwit-sim-stub/ directory.
    - Then replace this line in the dockerfile `FROM mcr.microsoft.com/dotnet/core/aspnet:8.0-buster-slim AS base`
    - now same folder as the Dockerfile build docker: `docker build -t Api_test .`
    - then run with `docker run api_test:latest`
    - then run: `docker run -p 8080:8080 api_test:latest`
    - Go to the link, and endpoints are visible as described in the .json file.
    - Now in a new terminal window run `python3 minitwit_simulator.py http://127.0.0.1:8080`. Make sure that the minitwit_scenario.csv file and minitwit_simulator.py file are in the same directory. The simulator should now run



15/02: 13:17
- added simulator tests.
- to run go into the MiniTwit.Web and use dotnet run, so the database gets build.
- Then go to the folder `out/itu-minitwit-sim-stub`.
- run `pytest minitwit_sim_api_test.py`
