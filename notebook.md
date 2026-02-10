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