## Load the words onto database

```bash

CREATE TABLE words (
    id SERIAL PRIMARY KEY,
    word VARCHAR(5)
);
```

```bash
\copy words(word) FROM 'words.txt' WITH (FORMAT csv)
```

verify:
```bash
SELECT * FROM words LIMIT 5;
```

create `users` and `sessions` table:

```bash
CREATE TABLE users (
    userid SERIAL PRIMARY KEY,
    username VARCHAR(20),
    password VARCHAR(20),
    createdat TIMESTAMP
);

CREATE TABLE sessions (
    sessionid SERIAL PRIMARY KEY,
    userid INT REFERENCES users(userid),
    score INT,
    playedat TIMESTAMP
);
```

```bash
dotnet add package Npgsql --version 8.0.9
```

## Results <br><br>
![Screenshot 1](./Screenshots/1.png)
![Screenshot 2](./Screenshots/2.png)
![Screenshot 3](./Screenshots/3.png)
![Screenshot 4](./Screenshots/4.png)

### DB Integration Results<br><br>
![Screenshot 5](./Screenshots/5.png)
![Screenshot 6](./Screenshots/6.png)
![Screenshot 7](./Screenshots/7.png)
![Screenshot 8](./Screenshots/8.png)

