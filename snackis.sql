PRAGMA foreign_keys=OFF;
BEGIN TRANSACTION;
CREATE TABLE IF NOT EXISTS "__EFMigrationsHistory" (
    "MigrationId" TEXT NOT NULL CONSTRAINT "PK___EFMigrationsHistory" PRIMARY KEY,
    "ProductVersion" TEXT NOT NULL
);
INSERT INTO __EFMigrationsHistory VALUES('20240523204437_InitialCreate','8.0.5');
INSERT INTO __EFMigrationsHistory VALUES('20240527143316_AddPhilosophers','8.0.5');
INSERT INTO __EFMigrationsHistory VALUES('20240530144838_ReactionsAdded','8.0.5');
INSERT INTO __EFMigrationsHistory VALUES('20240530172121_UserProfileAdded','8.0.5');
INSERT INTO __EFMigrationsHistory VALUES('20240530175627_GroupFunctionsAdded','8.0.5');
CREATE TABLE IF NOT EXISTS "AspNetRoles" (
    "Id" TEXT NOT NULL CONSTRAINT "PK_AspNetRoles" PRIMARY KEY,
    "Name" TEXT NULL,
    "NormalizedName" TEXT NULL,
    "ConcurrencyStamp" TEXT NULL
);
INSERT INTO AspNetRoles VALUES('ceed94b2-9864-4b3d-bde2-72f2dd99ca33','Admin','ADMIN',NULL);
INSERT INTO AspNetRoles VALUES('89cbb6a7-e364-4e9c-8f39-fd23f4ee457d','User','USER',NULL);
CREATE TABLE IF NOT EXISTS "AspNetUsers" (
    "Id" TEXT NOT NULL CONSTRAINT "PK_AspNetUsers" PRIMARY KEY,
    "ProfileImageUrl" TEXT NOT NULL,
    "UserName" TEXT NULL,
    "NormalizedUserName" TEXT NULL,
    "Email" TEXT NULL,
    "NormalizedEmail" TEXT NULL,
    "EmailConfirmed" INTEGER NOT NULL,
    "PasswordHash" TEXT NULL,
    "SecurityStamp" TEXT NULL,
    "ConcurrencyStamp" TEXT NULL,
    "PhoneNumber" TEXT NULL,
    "PhoneNumberConfirmed" INTEGER NOT NULL,
    "TwoFactorEnabled" INTEGER NOT NULL,
    "LockoutEnd" TEXT NULL,
    "LockoutEnabled" INTEGER NOT NULL,
    "AccessFailedCount" INTEGER NOT NULL
, "Bio" TEXT NOT NULL DEFAULT '', "Pseudonym" TEXT NOT NULL DEFAULT '');
INSERT INTO AspNetUsers VALUES('c99f91a4-5ec4-40b5-a436-c1be9189999b','/profileImages/cute-profile-picture-19mnx03g9yap3dhy.jpg','user.test2@mail.se','USER.TEST2@MAIL.SE','user.test2@mail.se','USER.TEST2@MAIL.SE',1,'AQAAAAIAAYagAAAAEDvrFOp6DS6pAGU32tv1ffiRuMcuRXzE6Mbzj6OS/IPJwjkCWdjURJAP8o03wcii8A==','APAAWULB4J2PFBEVONJX2V4BHALTUNSV','ae7f1208-1d70-439e-9c78-c04d400a49dd',NULL,0,0,NULL,1,0,'Hello!','YogaExpert');
INSERT INTO AspNetUsers VALUES('1','','user1','USER1','user1@example.com','USER1@EXAMPLE.COM',1,'passwordhash1','securitystamp1','concurrencystamp1',NULL,0,0,NULL,1,0,'','');
INSERT INTO AspNetUsers VALUES('2','','user2','USER2','user2@example.com','USER2@EXAMPLE.COM',1,'passwordhash2','securitystamp2','concurrencystamp2',NULL,0,0,NULL,1,0,'','');
INSERT INTO AspNetUsers VALUES('197bfa68-3c54-4077-a961-15941c6fb9dc','default-profile-image-url.jpg','admin@domain.com','ADMIN@DOMAIN.COM','admin@domain.com','ADMIN@DOMAIN.COM',0,'AQAAAAIAAYagAAAAEE+jpsT+HEzKVa1wK0feqCyk19w4Ampdzr8xCRHYrrBH/wVarfZ2FlGALAaG+2Smig==','H77XQUCDVWEXUEPLLG4GGF4C6XIC4OZ5','eb7696e5-7551-41c5-9acb-f3f92556f963',NULL,0,0,NULL,1,0,'','');
INSERT INTO AspNetUsers VALUES('306e9050-44bf-40eb-b155-91baf8e67043','/profileImages/Meditation-Yoga-Logo-7-1-580x348.jpg','admin@test.com','ADMIN@TEST.COM','admin@test.com','ADMIN@TEST.COM',1,'AQAAAAIAAYagAAAAEKOX0blX9YxhJwXz1Cmr+3rdm6dr+pthOiBTNZ2tmiIsADiY1zcEYg+tDuKnFBIxlA==','POJVUXIHR7ZV7WZK3VA5IGQFWWHOPCM5','ba50db90-9a0e-4299-b1de-fe0b7cd820ac',NULL,0,0,NULL,1,0,'Hello I''m admin!','Admin');
CREATE TABLE IF NOT EXISTS "Category" (
    "Id" INTEGER NOT NULL CONSTRAINT "PK_Category" PRIMARY KEY AUTOINCREMENT,
    "Name" TEXT NOT NULL
);
INSERT INTO Category VALUES(2,'Meditation');
INSERT INTO Category VALUES(3,'Mindfulness');
INSERT INTO Category VALUES(4,'Lifestyle');
INSERT INTO Category VALUES(5,'Community');
INSERT INTO Category VALUES(11,'Yoga');
CREATE TABLE IF NOT EXISTS "AspNetRoleClaims" (
    "Id" INTEGER NOT NULL CONSTRAINT "PK_AspNetRoleClaims" PRIMARY KEY AUTOINCREMENT,
    "RoleId" TEXT NOT NULL,
    "ClaimType" TEXT NULL,
    "ClaimValue" TEXT NULL,
    CONSTRAINT "FK_AspNetRoleClaims_AspNetRoles_RoleId" FOREIGN KEY ("RoleId") REFERENCES "AspNetRoles" ("Id") ON DELETE CASCADE
);
CREATE TABLE IF NOT EXISTS "AspNetUserClaims" (
    "Id" INTEGER NOT NULL CONSTRAINT "PK_AspNetUserClaims" PRIMARY KEY AUTOINCREMENT,
    "UserId" TEXT NOT NULL,
    "ClaimType" TEXT NULL,
    "ClaimValue" TEXT NULL,
    CONSTRAINT "FK_AspNetUserClaims_AspNetUsers_UserId" FOREIGN KEY ("UserId") REFERENCES "AspNetUsers" ("Id") ON DELETE CASCADE
);
CREATE TABLE IF NOT EXISTS "AspNetUserLogins" (
    "LoginProvider" TEXT NOT NULL,
    "ProviderKey" TEXT NOT NULL,
    "ProviderDisplayName" TEXT NULL,
    "UserId" TEXT NOT NULL,
    CONSTRAINT "PK_AspNetUserLogins" PRIMARY KEY ("LoginProvider", "ProviderKey"),
    CONSTRAINT "FK_AspNetUserLogins_AspNetUsers_UserId" FOREIGN KEY ("UserId") REFERENCES "AspNetUsers" ("Id") ON DELETE CASCADE
);
CREATE TABLE IF NOT EXISTS "AspNetUserRoles" (
    "UserId" TEXT NOT NULL,
    "RoleId" TEXT NOT NULL,
    CONSTRAINT "PK_AspNetUserRoles" PRIMARY KEY ("UserId", "RoleId"),
    CONSTRAINT "FK_AspNetUserRoles_AspNetRoles_RoleId" FOREIGN KEY ("RoleId") REFERENCES "AspNetRoles" ("Id") ON DELETE CASCADE,
    CONSTRAINT "FK_AspNetUserRoles_AspNetUsers_UserId" FOREIGN KEY ("UserId") REFERENCES "AspNetUsers" ("Id") ON DELETE CASCADE
);
INSERT INTO AspNetUserRoles VALUES('197bfa68-3c54-4077-a961-15941c6fb9dc','ceed94b2-9864-4b3d-bde2-72f2dd99ca33');
INSERT INTO AspNetUserRoles VALUES('306e9050-44bf-40eb-b155-91baf8e67043','ceed94b2-9864-4b3d-bde2-72f2dd99ca33');
CREATE TABLE IF NOT EXISTS "AspNetUserTokens" (
    "UserId" TEXT NOT NULL,
    "LoginProvider" TEXT NOT NULL,
    "Name" TEXT NOT NULL,
    "Value" TEXT NULL,
    CONSTRAINT "PK_AspNetUserTokens" PRIMARY KEY ("UserId", "LoginProvider", "Name"),
    CONSTRAINT "FK_AspNetUserTokens_AspNetUsers_UserId" FOREIGN KEY ("UserId") REFERENCES "AspNetUsers" ("Id") ON DELETE CASCADE
);
CREATE TABLE IF NOT EXISTS "Message" (
    "Id" INTEGER NOT NULL CONSTRAINT "PK_Message" PRIMARY KEY AUTOINCREMENT,
    "SenderId" TEXT NOT NULL,
    "RecipientId" TEXT NOT NULL,
    "Content" TEXT NOT NULL,
    "DateSent" TEXT NOT NULL,
    CONSTRAINT "FK_Message_AspNetUsers_RecipientId" FOREIGN KEY ("RecipientId") REFERENCES "AspNetUsers" ("Id") ON DELETE RESTRICT,
    CONSTRAINT "FK_Message_AspNetUsers_SenderId" FOREIGN KEY ("SenderId") REFERENCES "AspNetUsers" ("Id") ON DELETE RESTRICT
);
INSERT INTO Message VALUES(1,'306e9050-44bf-40eb-b155-91baf8e67043','c99f91a4-5ec4-40b5-a436-c1be9189999b','Hejsan svejsan','2024-05-24 18:15:36.493809');
INSERT INTO Message VALUES(2,'c99f91a4-5ec4-40b5-a436-c1be9189999b','306e9050-44bf-40eb-b155-91baf8e67043','hej du!','2024-05-24 18:16:44.08206');
INSERT INTO Message VALUES(3,'c99f91a4-5ec4-40b5-a436-c1be9189999b','c99f91a4-5ec4-40b5-a436-c1be9189999b','Till mig själv','2024-05-28 16:46:26.471916');
CREATE TABLE IF NOT EXISTS "SubCategory" (
    "Id" INTEGER NOT NULL CONSTRAINT "PK_SubCategory" PRIMARY KEY AUTOINCREMENT,
    "Name" TEXT NOT NULL,
    "CategoryId" INTEGER NOT NULL,
    CONSTRAINT "FK_SubCategory_Category_CategoryId" FOREIGN KEY ("CategoryId") REFERENCES "Category" ("Id") ON DELETE CASCADE
);
INSERT INTO SubCategory VALUES(8,'Meditation for Beginners',2);
INSERT INTO SubCategory VALUES(9,'Advanced Meditation',2);
INSERT INTO SubCategory VALUES(10,'Meditation Types',2);
INSERT INTO SubCategory VALUES(11,'Meditation and Health',2);
INSERT INTO SubCategory VALUES(12,'Meditation Tools',2);
INSERT INTO SubCategory VALUES(13,'What is Mindfulness?',3);
INSERT INTO SubCategory VALUES(14,'Mindfulness Techniques',3);
INSERT INTO SubCategory VALUES(15,'Mindfulness and Work Life',3);
INSERT INTO SubCategory VALUES(16,'Mindfulness for Children',3);
INSERT INTO SubCategory VALUES(17,'Mindfulness and Relationships',3);
INSERT INTO SubCategory VALUES(18,'Health and Well-being',4);
INSERT INTO SubCategory VALUES(19,'Mental Health',4);
INSERT INTO SubCategory VALUES(20,'Personal Development',4);
INSERT INTO SubCategory VALUES(21,'Holistic Health',4);
INSERT INTO SubCategory VALUES(22,'Introduce Yourself',5);
INSERT INTO SubCategory VALUES(23,'Events and Workshops',5);
INSERT INTO SubCategory VALUES(24,'Local Groups',5);
INSERT INTO SubCategory VALUES(25,'Feedback and Suggestions',5);
INSERT INTO SubCategory VALUES(76,'Wellness',4);
INSERT INTO SubCategory VALUES(77,'Yoga and daily life',11);
CREATE TABLE IF NOT EXISTS "Post" (
    "Id" INTEGER NOT NULL CONSTRAINT "PK_Post" PRIMARY KEY AUTOINCREMENT,
    "Title" TEXT NOT NULL,
    "Content" TEXT NOT NULL,
    "Image" TEXT NOT NULL,
    "Date" TEXT NOT NULL,
    "UserId" TEXT NOT NULL,
    "CategoryId" INTEGER NULL,
    "SubCategoryId" INTEGER NULL,
    CONSTRAINT "FK_Post_AspNetUsers_UserId" FOREIGN KEY ("UserId") REFERENCES "AspNetUsers" ("Id") ON DELETE CASCADE,
    CONSTRAINT "FK_Post_Category_CategoryId" FOREIGN KEY ("CategoryId") REFERENCES "Category" ("Id"),
    CONSTRAINT "FK_Post_SubCategory_SubCategoryId" FOREIGN KEY ("SubCategoryId") REFERENCES "SubCategory" ("Id")
);
INSERT INTO Post VALUES(14,'How to meditate','Give tips!','39225international-yoga-day-iphone-background-ktos7.webp','2024-05-28 17:08:42.873159','c99f91a4-5ec4-40b5-a436-c1be9189999b',2,8);
INSERT INTO Post VALUES(15,'My meditation post','Hello!','939797ac24345-1a6b-4a88-8dcd-fdfbf34a9ad6.webp','2024-05-28 17:10:35.382656','c99f91a4-5ec4-40b5-a436-c1be9189999b',2,8);
INSERT INTO Post VALUES(16,'Yoga','How to become a yoga teacher?','73404cutemessages.jpg','2024-05-29 11:45:20.152023','c99f91a4-5ec4-40b5-a436-c1be9189999b',11,77);
INSERT INTO Post VALUES(17,'hej','vgthyb','85227iconyoga.png','2024-05-30 16:22:12.310166','306e9050-44bf-40eb-b155-91baf8e67043',2,8);
CREATE TABLE IF NOT EXISTS "Comment" (
    "Id" INTEGER NOT NULL CONSTRAINT "PK_Comment" PRIMARY KEY AUTOINCREMENT,
    "PostId" INTEGER NOT NULL,
    "Content" TEXT NOT NULL,
    "Date" TEXT NOT NULL,
    "ParentCommentId" INTEGER NULL,
    CONSTRAINT "FK_Comment_Comment_ParentCommentId" FOREIGN KEY ("ParentCommentId") REFERENCES "Comment" ("Id"),
    CONSTRAINT "FK_Comment_Post_PostId" FOREIGN KEY ("PostId") REFERENCES "Post" ("Id") ON DELETE CASCADE
);
INSERT INTO Comment VALUES(9,14,'Hej du!','2024-05-28 19:09:14.239803',NULL);
INSERT INTO Comment VALUES(10,14,'Hej igen!','2024-05-28 19:09:22.360687',9);
INSERT INTO Comment VALUES(11,15,'Hej','2024-05-29 13:21:57.960037',NULL);
INSERT INTO Comment VALUES(12,15,'hej igen','2024-05-29 13:22:26.380452',11);
INSERT INTO Comment VALUES(13,14,'hej','2024-05-29 13:22:59.403847',NULL);
INSERT INTO Comment VALUES(14,16,'Hej!','2024-05-30 18:55:22.909858',NULL);
INSERT INTO Comment VALUES(15,17,'****','2024-05-30 19:08:19.461527',NULL);
INSERT INTO Comment VALUES(16,17,'****','2024-05-30 19:08:25.292742',NULL);
INSERT INTO Comment VALUES(17,15,replace(replace('hej igen 2\r\n','\r',char(13)),'\n',char(10)),'2024-05-31 12:41:19.669768',12);
INSERT INTO Comment VALUES(18,17,'hej','2024-05-31 13:36:35.087391',NULL);
INSERT INTO Comment VALUES(19,17,'Hej','2024-05-31 13:39:26.796659',18);
INSERT INTO Comment VALUES(20,17,'Hej igen','2024-05-31 13:39:33.791201',NULL);
INSERT INTO Comment VALUES(21,17,'Hej 2','2024-05-31 13:39:43.856021',20);
INSERT INTO Comment VALUES(22,17,'Hej 3','2024-05-31 13:39:49.424089',21);
INSERT INTO Comment VALUES(23,17,'hej 3','2024-05-31 13:39:58.056348',21);
INSERT INTO Comment VALUES(24,17,'hej ige 2','2024-05-31 13:40:05.523642',20);
INSERT INTO Comment VALUES(25,17,'Inte svära!','2024-05-31 13:41:47.931597',15);
INSERT INTO Comment VALUES(26,17,'Hej3','2024-05-31 13:42:36.218815',24);
INSERT INTO Comment VALUES(27,17,'halloj','2024-05-31 13:43:59.385735',21);
INSERT INTO Comment VALUES(28,17,'hej','2024-05-31 13:44:52.875117',24);
INSERT INTO Comment VALUES(29,17,'Hej','2024-05-31 13:45:47.029435',24);
INSERT INTO Comment VALUES(30,17,'hej 3','2024-05-31 13:49:31.229643',19);
INSERT INTO Comment VALUES(31,16,'hej 2','2024-05-31 14:01:44.631803',14);
INSERT INTO Comment VALUES(32,16,'Hej 3','2024-05-31 14:01:50.644134',31);
INSERT INTO Comment VALUES(33,16,'hej 3','2024-05-31 14:03:59.876986',NULL);
INSERT INTO Comment VALUES(34,16,'hej 2.3','2024-05-31 14:04:14.147736',31);
INSERT INTO Comment VALUES(35,16,'Hej 3.2','2024-05-31 14:04:20.504688',33);
INSERT INTO Comment VALUES(36,16,'Hej 3.3','2024-05-31 14:04:28.132084',35);
INSERT INTO Comment VALUES(37,16,'Hej 3.4','2024-05-31 15:37:33.192287',36);
INSERT INTO Comment VALUES(38,16,'hej 3.1','2024-05-31 15:37:47.8795',33);
INSERT INTO Comment VALUES(39,16,'hej test','2024-05-31 15:39:23.113466',NULL);
CREATE TABLE IF NOT EXISTS "Reports" (
    "Id" INTEGER NOT NULL CONSTRAINT "PK_Reports" PRIMARY KEY AUTOINCREMENT,
    "PostId" INTEGER NOT NULL,
    "ReportedById" TEXT NOT NULL,
    "ReportDate" TEXT NOT NULL,
    "Reason" TEXT NOT NULL,
    "Status" INTEGER NOT NULL DEFAULT 0,
    CONSTRAINT "FK_Reports_AspNetUsers_ReportedById" FOREIGN KEY ("ReportedById") REFERENCES "AspNetUsers" ("Id") ON DELETE CASCADE,
    CONSTRAINT "FK_Reports_Post_PostId" FOREIGN KEY ("PostId") REFERENCES "Post" ("Id") ON DELETE CASCADE
);
INSERT INTO Reports VALUES(2,15,'c99f91a4-5ec4-40b5-a436-c1be9189999b','2024-05-29 13:05:47.49218','Inappropriate content',2);
INSERT INTO Reports VALUES(3,14,'306e9050-44bf-40eb-b155-91baf8e67043','2024-05-30 15:35:28.04623','Inappropriate content',0);
INSERT INTO Reports VALUES(4,17,'306e9050-44bf-40eb-b155-91baf8e67043','2024-05-30 16:22:24.400153','Inappropriate content',0);
INSERT INTO Reports VALUES(5,16,'306e9050-44bf-40eb-b155-91baf8e67043','2024-05-30 18:55:17.682468','Inappropriate content',0);
CREATE TABLE IF NOT EXISTS "Philosopher" (
    "Id" INTEGER NOT NULL CONSTRAINT "PK_Philosopher" PRIMARY KEY AUTOINCREMENT,
    "Name" TEXT NOT NULL,
    "Quote" TEXT NOT NULL
);
INSERT INTO Philosopher VALUES(1,'Socrates','The unexamined life is not worth living.');
INSERT INTO Philosopher VALUES(2,'Plato','Wise men speak because they have something to say; fools because they have to say something.');
INSERT INTO Philosopher VALUES(3,'Aristotle','Knowing yourself is the beginning of all wisdom.');
CREATE TABLE IF NOT EXISTS "Reactions" (
    "Id" INTEGER NOT NULL CONSTRAINT "PK_Reactions" PRIMARY KEY AUTOINCREMENT,
    "PostId" INTEGER NOT NULL,
    "ReactedbyId" TEXT NOT NULL,
    "ReactionType" TEXT NOT NULL,
    "CreatedAt" TEXT NOT NULL,
    CONSTRAINT "FK_Reactions_AspNetUsers_ReactedbyId" FOREIGN KEY ("ReactedbyId") REFERENCES "AspNetUsers" ("Id") ON DELETE CASCADE,
    CONSTRAINT "FK_Reactions_Post_PostId" FOREIGN KEY ("PostId") REFERENCES "Post" ("Id") ON DELETE CASCADE
);
INSERT INTO Reactions VALUES(1,17,'306e9050-44bf-40eb-b155-91baf8e67043','heart','2024-05-30 18:44:12.268944');
INSERT INTO Reactions VALUES(2,17,'306e9050-44bf-40eb-b155-91baf8e67043','thumbs-up','2024-05-30 18:44:13.264382');
INSERT INTO Reactions VALUES(3,17,'306e9050-44bf-40eb-b155-91baf8e67043','thumbs-up','2024-05-30 18:44:13.711469');
INSERT INTO Reactions VALUES(4,17,'306e9050-44bf-40eb-b155-91baf8e67043','thumbs-up','2024-05-30 18:44:14.19544');
INSERT INTO Reactions VALUES(5,17,'306e9050-44bf-40eb-b155-91baf8e67043','heart','2024-05-30 18:44:14.845858');
INSERT INTO Reactions VALUES(6,16,'306e9050-44bf-40eb-b155-91baf8e67043','heart','2024-05-30 18:55:12.199118');
INSERT INTO Reactions VALUES(7,15,'c99f91a4-5ec4-40b5-a436-c1be9189999b','thumbs-up','2024-05-31 12:44:18.846005');
INSERT INTO Reactions VALUES(8,17,'c99f91a4-5ec4-40b5-a436-c1be9189999b','thumbs-up','2024-05-31 13:24:14.68815');
INSERT INTO Reactions VALUES(9,17,'c99f91a4-5ec4-40b5-a436-c1be9189999b','heart','2024-05-31 13:35:57.567133');
INSERT INTO Reactions VALUES(10,16,'c99f91a4-5ec4-40b5-a436-c1be9189999b','thumbs-up','2024-05-31 14:01:55.79075');
INSERT INTO Reactions VALUES(11,16,'c99f91a4-5ec4-40b5-a436-c1be9189999b','thumbs-up','2024-05-31 15:39:29.607712');
INSERT INTO Reactions VALUES(12,16,'c99f91a4-5ec4-40b5-a436-c1be9189999b','heart','2024-05-31 15:39:30.480628');
INSERT INTO Reactions VALUES(13,15,'c99f91a4-5ec4-40b5-a436-c1be9189999b','heart','2024-06-02 17:03:42.523644');
INSERT INTO Reactions VALUES(14,15,'c99f91a4-5ec4-40b5-a436-c1be9189999b','thumbs-up','2024-06-02 17:03:43.035609');
INSERT INTO Reactions VALUES(15,15,'c99f91a4-5ec4-40b5-a436-c1be9189999b','thumbs-up','2024-06-02 17:03:43.352088');
INSERT INTO Reactions VALUES(16,15,'c99f91a4-5ec4-40b5-a436-c1be9189999b','heart','2024-06-02 17:03:44.023474');
CREATE TABLE IF NOT EXISTS "Groups" (
    "Id" INTEGER NOT NULL CONSTRAINT "PK_Groups" PRIMARY KEY AUTOINCREMENT,
    "Name" TEXT NOT NULL,
    "CreatorId" TEXT NOT NULL,
    CONSTRAINT "FK_Groups_AspNetUsers_CreatorId" FOREIGN KEY ("CreatorId") REFERENCES "AspNetUsers" ("Id") ON DELETE CASCADE
);
INSERT INTO "Groups" VALUES(1,'Yoga lovers','306e9050-44bf-40eb-b155-91baf8e67043');
INSERT INTO "Groups" VALUES(2,'Meditation enthusiasts','306e9050-44bf-40eb-b155-91baf8e67043');
INSERT INTO "Groups" VALUES(3,'my group','c99f91a4-5ec4-40b5-a436-c1be9189999b');
CREATE TABLE IF NOT EXISTS "GroupInvitations" (
    "GroupId" INTEGER NOT NULL,
    "InvitedUserId" TEXT NOT NULL,
    "Id" INTEGER NOT NULL,
    "InvitingUserId" TEXT NOT NULL,
    "Timestamp" TEXT NOT NULL,
    "Accepted" INTEGER NOT NULL,
    CONSTRAINT "PK_GroupInvitations" PRIMARY KEY ("GroupId", "InvitedUserId"),
    CONSTRAINT "FK_GroupInvitations_AspNetUsers_InvitedUserId" FOREIGN KEY ("InvitedUserId") REFERENCES "AspNetUsers" ("Id") ON DELETE CASCADE,
    CONSTRAINT "FK_GroupInvitations_AspNetUsers_InvitingUserId" FOREIGN KEY ("InvitingUserId") REFERENCES "AspNetUsers" ("Id") ON DELETE CASCADE,
    CONSTRAINT "FK_GroupInvitations_Groups_GroupId" FOREIGN KEY ("GroupId") REFERENCES "Groups" ("Id") ON DELETE CASCADE
);
INSERT INTO GroupInvitations VALUES(1,'c99f91a4-5ec4-40b5-a436-c1be9189999b',0,'306e9050-44bf-40eb-b155-91baf8e67043','2024-05-30 22:20:46.317735',1);
INSERT INTO GroupInvitations VALUES(1,'1',0,'306e9050-44bf-40eb-b155-91baf8e67043','2024-05-30 22:21:08.507017',0);
INSERT INTO GroupInvitations VALUES(3,'1',0,'c99f91a4-5ec4-40b5-a436-c1be9189999b','2024-05-30 22:23:55.883128',0);
INSERT INTO GroupInvitations VALUES(3,'197bfa68-3c54-4077-a961-15941c6fb9dc',0,'c99f91a4-5ec4-40b5-a436-c1be9189999b','2024-05-30 22:24:12.684632',0);
CREATE TABLE IF NOT EXISTS "GroupMembers" (
    "GroupId" INTEGER NOT NULL,
    "UserId" TEXT NOT NULL,
    CONSTRAINT "PK_GroupMembers" PRIMARY KEY ("GroupId", "UserId"),
    CONSTRAINT "FK_GroupMembers_AspNetUsers_UserId" FOREIGN KEY ("UserId") REFERENCES "AspNetUsers" ("Id") ON DELETE CASCADE,
    CONSTRAINT "FK_GroupMembers_Groups_GroupId" FOREIGN KEY ("GroupId") REFERENCES "Groups" ("Id") ON DELETE CASCADE
);
INSERT INTO GroupMembers VALUES(1,'306e9050-44bf-40eb-b155-91baf8e67043');
INSERT INTO GroupMembers VALUES(2,'306e9050-44bf-40eb-b155-91baf8e67043');
INSERT INTO GroupMembers VALUES(1,'c99f91a4-5ec4-40b5-a436-c1be9189999b');
CREATE TABLE IF NOT EXISTS "GroupMessages" (
    "Id" INTEGER NOT NULL CONSTRAINT "PK_GroupMessages" PRIMARY KEY AUTOINCREMENT,
    "GroupId" INTEGER NOT NULL,
    "UserId" TEXT NOT NULL,
    "Content" TEXT NOT NULL,
    "Timestamp" TEXT NOT NULL,
    CONSTRAINT "FK_GroupMessages_AspNetUsers_UserId" FOREIGN KEY ("UserId") REFERENCES "AspNetUsers" ("Id") ON DELETE CASCADE,
    CONSTRAINT "FK_GroupMessages_Groups_GroupId" FOREIGN KEY ("GroupId") REFERENCES "Groups" ("Id") ON DELETE CASCADE
);
INSERT INTO GroupMessages VALUES(1,1,'306e9050-44bf-40eb-b155-91baf8e67043','hej','2024-05-30 22:21:20.54017');
INSERT INTO GroupMessages VALUES(2,3,'c99f91a4-5ec4-40b5-a436-c1be9189999b','hej gruppen','2024-05-30 22:24:19.385616');
INSERT INTO GroupMessages VALUES(3,1,'c99f91a4-5ec4-40b5-a436-c1be9189999b','hej','2024-05-30 22:45:49.179787');
DELETE FROM sqlite_sequence;
INSERT INTO sqlite_sequence VALUES('Category',13);
INSERT INTO sqlite_sequence VALUES('SubCategory',83);
INSERT INTO sqlite_sequence VALUES('Post',17);
INSERT INTO sqlite_sequence VALUES('Comment',39);
INSERT INTO sqlite_sequence VALUES('Reports',5);
INSERT INTO sqlite_sequence VALUES('Message',3);
INSERT INTO sqlite_sequence VALUES('Philosopher',3);
INSERT INTO sqlite_sequence VALUES('Reactions',16);
INSERT INTO sqlite_sequence VALUES('Groups',3);
INSERT INTO sqlite_sequence VALUES('GroupMessages',3);
CREATE INDEX "IX_AspNetRoleClaims_RoleId" ON "AspNetRoleClaims" ("RoleId");
CREATE UNIQUE INDEX "RoleNameIndex" ON "AspNetRoles" ("NormalizedName");
CREATE INDEX "IX_AspNetUserClaims_UserId" ON "AspNetUserClaims" ("UserId");
CREATE INDEX "IX_AspNetUserLogins_UserId" ON "AspNetUserLogins" ("UserId");
CREATE INDEX "IX_AspNetUserRoles_RoleId" ON "AspNetUserRoles" ("RoleId");
CREATE INDEX "EmailIndex" ON "AspNetUsers" ("NormalizedEmail");
CREATE UNIQUE INDEX "UserNameIndex" ON "AspNetUsers" ("NormalizedUserName");
CREATE INDEX "IX_Comment_ParentCommentId" ON "Comment" ("ParentCommentId");
CREATE INDEX "IX_Comment_PostId" ON "Comment" ("PostId");
CREATE INDEX "IX_Message_RecipientId" ON "Message" ("RecipientId");
CREATE INDEX "IX_Message_SenderId" ON "Message" ("SenderId");
CREATE INDEX "IX_Post_CategoryId" ON "Post" ("CategoryId");
CREATE INDEX "IX_Post_SubCategoryId" ON "Post" ("SubCategoryId");
CREATE INDEX "IX_Post_UserId" ON "Post" ("UserId");
CREATE INDEX "IX_Reports_PostId" ON "Reports" ("PostId");
CREATE INDEX "IX_Reports_ReportedById" ON "Reports" ("ReportedById");
CREATE INDEX "IX_SubCategory_CategoryId" ON "SubCategory" ("CategoryId");
CREATE INDEX "IX_Reactions_PostId" ON "Reactions" ("PostId");
CREATE INDEX "IX_Reactions_ReactedbyId" ON "Reactions" ("ReactedbyId");
CREATE INDEX "IX_GroupInvitations_InvitedUserId" ON "GroupInvitations" ("InvitedUserId");
CREATE INDEX "IX_GroupInvitations_InvitingUserId" ON "GroupInvitations" ("InvitingUserId");
CREATE INDEX "IX_GroupMembers_UserId" ON "GroupMembers" ("UserId");
CREATE INDEX "IX_GroupMessages_GroupId" ON "GroupMessages" ("GroupId");
CREATE INDEX "IX_GroupMessages_UserId" ON "GroupMessages" ("UserId");
CREATE INDEX "IX_Groups_CreatorId" ON "Groups" ("CreatorId");
COMMIT;
