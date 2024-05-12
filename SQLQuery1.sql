CREATE TABLE [dbo].[Item] (
    [Item_Id]        INT           NOT NULL,
    [name]           VARCHAR (30)  NULL,
    [price]          FLOAT (53)    NULL,
    [original_price] FLOAT (53)    NULL,
    [type]           VARCHAR (50)  NULL,
    [bought_date]    DATETIME      NULL,
    [description]    VARCHAR (200) NULL,
    [status]         VARCHAR (200) NULL,
    [image_path]     VARCHAR (200) NULL,
    [Id_user]        INT           NOT NULL,
    PRIMARY KEY CLUSTERED ([Item_Id] ASC),
    CONSTRAINT [FK_Id_user] FOREIGN KEY ([Id_user]) REFERENCES [dbo].[User] ([Id_user])
);

