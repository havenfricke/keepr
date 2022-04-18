CREATE TABLE IF NOT EXISTS accounts(
  id VARCHAR(255) NOT NULL primary key COMMENT 'primary key',
  createdAt DATETIME DEFAULT CURRENT_TIMESTAMP COMMENT 'Time Created',
  updatedAt DATETIME DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP COMMENT 'Last Update',
  name varchar(255) COMMENT 'User Name',
  email varchar(255) COMMENT 'User Email',
  picture varchar(255) COMMENT 'User Picture'
) default charset utf8 COMMENT '';
CREATE TABLE IF NOT EXISTS vaults(
  id INT NOT NULL AUTO_INCREMENT primary key COMMENT 'primary key',
  createdAt DATETIME DEFAULT CURRENT_TIMESTAMP COMMENT 'Time Created',
  updatedAt DATETIME DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP COMMENT 'Last Update',
  name VARCHAR(55) NOT NULL,
  description VARCHAR(255) NOT NULL,
  isPrivate BOOLEAN NOT NULL DEFAULT 0,
  creatorId VARCHAR(255) NOT NULL,
  FOREIGN KEY (creatorId) REFERENCES accounts(id) ON DELETE CASCADE
) default charset utf8 COMMENT '';
CREATE TABLE IF NOT EXISTS keeps(
  id INT NOT NULL AUTO_INCREMENT primary key COMMENT 'primary key',
  createdAt DATETIME DEFAULT CURRENT_TIMESTAMP COMMENT 'Time Created',
  updatedAt DATETIME DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP COMMENT 'Last Update',
  name VARCHAR(55) NOT NULL,
  description VARCHAR(255) NOT NULL,
  img TEXT NOT NULL,
  views INT NOT NULL DEFAULT 0,
  kept INT NOT NULL DEFAULT 0,
  creatorId VARCHAR(255),
  FOREIGN KEY (creatorId) REFERENCES accounts(id) ON DELETE CASCADE
) default charset utf8 COMMENT '';
CREATE TABLE IF NOT EXISTS vaultkeeps(
  id INT NOT NULL AUTO_INCREMENT primary key COMMENT 'primary key',
  createdAt DATETIME DEFAULT CURRENT_TIMESTAMP COMMENT 'Time Created',
  updatedAt DATETIME DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP COMMENT 'Last Update',
  keepId INT NOT NULL,
  vaultId INT NOT NULL,
  creatorId VARCHAR(255) NOT NULL,
  FOREIGN KEY (keepId) REFERENCES keeps(id) ON DELETE CASCADE,
  FOREIGN KEY (vaultId) REFERENCES vaults(id) ON DELETE CASCADE,
  FOREIGN KEY (creatorId) REFERENCES accounts(id) ON DELETE CASCADE
) default charset utf8 COMMENT '';
INSERT INTO
  keeps (name, description, img, views, kept, creatorId)
VALUES
  (
    'Thing',
    'This is a test thing',
    'https://thiscatdoesnotexist.com',
    0,
    0,
    '6238c27e986c5f4f83aa4099'
  );
SELECT
  LAST_INSERT_ID();
SELECT
  *
FROM
  keeps;
UPDATE
  keeps
SET
  views = views + 1
WHERE
  id = 19;
SELECT
  k.*,
  a.*
FROM
  keeps k
  JOIN accounts a ON k.creatorId = a.id
WHERE
  k.id = 19;
SELECT
  kv.*,
  k.*
FROM
  vaultkeeps kv
  JOIN keeps k ON k.id = kv.id
WHERE
  kv.vaultId = 1;
SELECT
  kv.*,
  kv.vaultId AS VaultKeepId,
  k.*,
  a.*
FROM
  vaultkeeps kv
  JOIN keeps k ON k.id = kv.keepId
  JOIN accounts a ON k.creatorId = a.id
WHERE
  kv.vaultId = 75;
SELECT
  *
FROM
  vaultkeeps
WHERE
  id = 3;
SELECT
  *
FROM
  vaultkeeps;
SELECT
  *
FROM
  accounts
WHERE
  id = '6238c27e986c5f4f83aa4099';
SELECT
  *
FROM
  keeps
WHERE
  creatorId = '6238c27e986c5f4f83aa4099';
UPDATE
  keeps
SET
  kept = kept + 1;
WHERE
  id = 75;
SELECT
  kv.*,
  kv.id AS vaultkeepId,
  k.*,
  a.*,
  v.*
FROM
  vaultkeeps kv
  JOIN keeps k ON k.id = kv.keepId
  JOIN accounts a ON k.creatorId = a.id
  JOIN vaults v ON kv.VaultId = v.id
WHERE
  kv.vaultId = 57;
SELECT
  *
FROM
  vaultkeeps
WHERE
  id = 21;