CREATE TABLE [dbo].[Users] (
    [UserId]     NVARCHAR (450) NOT NULL,
    [Age]        INT            NULL,
    [Gender]     NVARCHAR (25)  NOT NULL,
    [UserStatus] INT            NOT NULL,
    [Banned]     BIT            NOT NULL,
    CONSTRAINT [PK_Users] PRIMARY KEY CLUSTERED ([UserId] ASC),
    CONSTRAINT [FK_Users_AspNetUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [dbo].[AspNetUsers] ([Id]) ON DELETE CASCADE
);

