DROP TABLE IF EXISTS "Users";
DROP SEQUENCE IF EXISTS "Users_UserId_seq";
CREATE SEQUENCE "Users_UserId_seq" INCREMENT  MINVALUE  MAXVALUE  CACHE ;

CREATE TABLE "public"."Users" (
    "Id" text NOT NULL,
    "UserId" integer GENERATED ALWAYS AS IDENTITY,
    "Age" integer,
    "Gender" character varying(20) NOT NULL,
    "UserStatus" integer NOT NULL,
    "Banned" boolean NOT NULL,
    CONSTRAINT "Users_UserId" PRIMARY KEY ("UserId")
) WITH (oids = false);


ALTER TABLE ONLY "public"."Users" ADD CONSTRAINT "FK_Users_Id" FOREIGN KEY ("Id") REFERENCES "AspNetUsers"("Id") ON UPDATE CASCADE ON DELETE CASCADE NOT DEFERRABLE; 
