CREATE TABLE IF NOT EXISTS accounts(
  id VARCHAR(255) NOT NULL primary key COMMENT 'primary key',
  createdAt DATETIME DEFAULT CURRENT_TIMESTAMP COMMENT 'Time Created',
  updatedAt DATETIME DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP COMMENT 'Last Update',
  name varchar(255) COMMENT 'User Name',
  email varchar(255) COMMENT 'User Email',
  picture varchar(255) COMMENT 'User Picture'
) default charset utf8 COMMENT '';

CREATE TABLE IF NOT EXISTS recipes(
  id int AUTO_INCREMENT primary key,
  creatorId varchar(255) NOT NULL,
  createdAt DATETIME DEFAULT CURRENT_TIMESTAMP COMMENT 'Time Created',
  updatedAt DATETIME DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP COMMENT 'Last Update',
  title varchar(255) COMMENT 'Title',
  subtitle varchar(255) COMMENT 'Subtitle',
  category varchar(255) COMMENT 'Category',
  Picture TEXT
) default charset utf8 COMMENT '';
CREATE TABLE IF NOT EXISTS ingredients(
  id int AUTO_INCREMENT primary key,
  recipeId INT,
  createdAt DATETIME DEFAULT CURRENT_TIMESTAMP COMMENT 'Time Created',
  updatedAt DATETIME DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP COMMENT 'Last Update',
  name varchar(255),
  quantity varchar(255)
) default charset utf8 COMMENT '';
CREATE TABLE IF NOT EXISTS steps(
  id int AUTO_INCREMENT primary key,
  recipeId INT,
  createdAt DATETIME DEFAULT CURRENT_TIMESTAMP COMMENT 'Time Created',
  updatedAt DATETIME DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP COMMENT 'Last Update',
  position INT ,
 body TEXT
) default charset utf8 COMMENT '';
CREATE TABLE IF NOT EXISTS favorites(
  id INT AUTO_INCREMENT primary key,
  recipeId INT,
  accountId VARCHAR(255),
  FOREIGN   KEY (recipeId) REFERENCES recipes(id) ON DELETE CASCADE,
  FOREIGN KEY (accountId) REFERENCES accounts(id) ON DELETE CASCADE
) default charset utf8 COMMENT '';
