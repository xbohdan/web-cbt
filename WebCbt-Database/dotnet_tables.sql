DROP TABLE IF EXISTS "AspNetRoles";
CREATE TABLE "public"."AspNetRoles" (
    "Id" text NOT NULL,
    "Name" character varying(256),
    "NormalizedName" character varying(256),
    "ConcurrencyStamp" text,
    CONSTRAINT "PK_AspNetRoles" PRIMARY KEY ("Id"),
    CONSTRAINT "RoleNameIndex" UNIQUE ("NormalizedName")
) WITH (oids = false);


DROP TABLE IF EXISTS "AspNetUserLogins";
CREATE TABLE "public"."AspNetUserLogins" (
    "LoginProvider" character varying(128) NOT NULL,
    "ProviderKey" character varying(128) NOT NULL,
    "ProviderDisplayName" text,
    "UserId" text NOT NULL,
    CONSTRAINT "PK_AspNetUserLogins" PRIMARY KEY ("LoginProvider", "ProviderKey")
) WITH (oids = false);

CREATE INDEX "IX_AspNetUserLogins_UserId" ON "public"."AspNetUserLogins"USING btree ("UserId");


DROP TABLE IF EXISTS "AspNetUserRoles";
CREATE TABLE "public"."AspNetUserRoles" (
    "UserId" text NOT NULL,
    "RoleId" text NOT NULL,
    CONSTRAINT "PK_AspNetUserRoles" PRIMARY KEY ("UserId", "RoleId")
) WITH (oids = false);

CREATE INDEX "IX_AspNetUserRoles_RoleId" ON "public"."AspNetUserRoles" USING btree ("RoleId");


DROP TABLE IF EXISTS "AspNetUserTokens";
CREATE TABLE "public"."AspNetUserTokens" (
    "UserId" text NOT NULL,
    "LoginProvider" character varying(128) NOT NULL,
    "Name" character varying(128) NOT NULL,
    "Value" text,
    CONSTRAINT "PK_AspNetUserTokens" PRIMARY KEY ("UserId", "LoginProvider", "Name")
) WITH (oids = false);


DROP TABLE IF EXISTS "AspNetUsers";
CREATE TABLE "public"."AspNetUsers" (
    "Id" text NOT NULL,
    "UserName" character varying(256),
    "NormalizedUserName" character varying(256),
    "Email" character varying(256),
    "NormalizedEmail" character varying(256),
    "EmailConfirmed" boolean NOT NULL,
    "PasswordHash" text,
    "SecurityStamp" text,
    "ConcurrencyStamp" text,
    "PhoneNumber" text,
    "PhoneNumberConfirmed" boolean NOT NULL,
    "TwoFactorEnabled" boolean NOT NULL,
    "LockoutEnd" timestamptz,
    "LockoutEnabled" boolean NOT NULL,
    "AccessFailedCount" integer NOT NULL,
    CONSTRAINT "PK_AspNetUsers" PRIMARY KEY ("Id"),
    CONSTRAINT "UserNameIndex" UNIQUE ("NormalizedUserName")
) WITH (oids = false);

CREATE INDEX "EmailIndex" ON "public"."AspNetUsers" USING btree ("NormalizedEmail");

ALTER TABLE ONLY "public"."AspNetUserLogins" ADD CONSTRAINT "FK_AspNetUserLogins_AspNetUsers_UserId" FOREIGN KEY ("UserId") REFERENCES "AspNetUsers"("Id") ON DELETE CASCADE NOT DEFERRABLE;

ALTER TABLE ONLY "public"."AspNetUserRoles" ADD CONSTRAINT "FK_AspNetUserRoles_AspNetRoles_RoleId" FOREIGN KEY ("RoleId") REFERENCES "AspNetRoles"("Id") ON DELETE CASCADE NOT DEFERRABLE;
ALTER TABLE ONLY "public"."AspNetUserRoles" ADD CONSTRAINT "FK_AspNetUserRoles_AspNetUsers_UserId" FOREIGN KEY ("UserId") REFERENCES "AspNetUsers"("Id") ON DELETE CASCADE NOT DEFERRABLE;

ALTER TABLE ONLY "public"."AspNetUserTokens" ADD CONSTRAINT "FK_AspNetUserTokens_AspNetUsers_UserId" FOREIGN KEY ("UserId") REFERENCES "AspNetUsers"("Id") ON DELETE CASCADE NOT DEFERRABLE;
