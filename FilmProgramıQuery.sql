CREATE TABLE [dbo].[Film] (
    [FilmId]        INT            IDENTITY (1, 1) NOT NULL,
    [FilmAdı]       NVARCHAR (MAX) NOT NULL,
    [FilmKonu]      NVARCHAR (MAX) NOT NULL,
    [FilmThumb]     NVARCHAR (MAX) NOT NULL,
    [FilmKategori]  NVARCHAR (MAX) NULL,
    [FilmFragman]   NVARCHAR (MAX) NULL,
    [FilmOrjinalAd] NVARCHAR (MAX) NULL,
    [FilmCıkısYılı] NVARCHAR (50)  NULL,
    [imdbRating]    NVARCHAR (MAX) NULL,
    PRIMARY KEY CLUSTERED ([FilmId] ASC)
);

CREATE TABLE [dbo].[Oyuncu] (
    [OyuncuId]    INT           IDENTITY (1, 1) NOT NULL,
    [OyuncuAd]    NVARCHAR (30) NOT NULL,
    [OyuncuSoyad] NVARCHAR (30) NOT NULL,
    PRIMARY KEY CLUSTERED ([OyuncuId] ASC)
);

CREATE TABLE [dbo].[FilmdekiOyuncular] (
    [Id]       INT IDENTITY (1, 1) NOT NULL,
    [FilmId]   INT NOT NULL,
    [OyuncuId] INT NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_FilmdekiOyuncular_To_Film] FOREIGN KEY ([FilmId]) REFERENCES [dbo].[Film] ([FilmId]),
    CONSTRAINT [FK_FilmdekiOyuncular_To_Oyuncu] FOREIGN KEY ([OyuncuId]) REFERENCES [dbo].[Oyuncu] ([OyuncuId])
);

CREATE TABLE [dbo].[Kullanıcı] (
    [KullaniciId]  INT            IDENTITY (1, 1) NOT NULL,
    [Ad]           NVARCHAR (MAX) NOT NULL,
    [Soyad]        NVARCHAR (MAX) NOT NULL,
    [Yas]          INT            NOT NULL,
    [Cinsiyet]     NVARCHAR (10)  NOT NULL,
    [KullanıcıAdı] NVARCHAR (MAX) NOT NULL,
    [Sifre]        NVARCHAR (MAX) NOT NULL,
    PRIMARY KEY CLUSTERED ([KullaniciId] ASC)
);

CREATE TABLE [dbo].[Listem] (
    [ListeId]     INT IDENTITY (1, 1) NOT NULL,
    [FilmId]      INT NOT NULL,
    [KullanıcıId] INT NOT NULL,
    PRIMARY KEY CLUSTERED ([ListeId] ASC),
    CONSTRAINT [FK_Listem_To_Film] FOREIGN KEY ([FilmId]) REFERENCES [dbo].[Film] ([FilmId]),
    CONSTRAINT [FK_Listem_To_Kullanıcı] FOREIGN KEY ([KullanıcıId]) REFERENCES [dbo].[Kullanıcı] ([KullaniciId])
);

CREATE TABLE [dbo].[OzelListe] (
    [OzelListeId] INT            IDENTITY (1, 1) NOT NULL,
    [FilmAdı]     NVARCHAR (MAX) NULL,
    [CıkısYılı]   NVARCHAR (MAX) NULL,
    [Yönetmen]    NVARCHAR (MAX) NULL,
    [Puan]        NVARCHAR (MAX) NULL,
    [OylayanSayı] NVARCHAR (MAX) NULL,
    [Oyuncular]   NVARCHAR (MAX) NULL,
    [Kategori]    NVARCHAR (MAX) NULL,
    [Konu]        NVARCHAR (MAX) NULL,
    [KullanıcıId] INT            NOT NULL,
    [PosterYol]   NVARCHAR (MAX) NULL,
    PRIMARY KEY CLUSTERED ([OzelListeId] ASC),
    CONSTRAINT [FK_OzelListe_To_Kullanıcı] FOREIGN KEY ([KullanıcıId]) REFERENCES [dbo].[Kullanıcı] ([KullaniciId])
);

CREATE TABLE [dbo].[Top20] (
    [Top20Id] INT IDENTITY (1, 1) NOT NULL,
    [FilmId]  INT NOT NULL,
    PRIMARY KEY CLUSTERED ([Top20Id] ASC),
    CONSTRAINT [FK_Top20_Film] FOREIGN KEY ([FilmId]) REFERENCES [dbo].[Film] ([FilmId])
);

CREATE TABLE [dbo].[Yorum] (
    [YorumId]      INT            IDENTITY (1, 1) NOT NULL,
    [YorumMetin]   NVARCHAR (MAX) NOT NULL,
    [YorumYazanId] INT            NOT NULL,
    [YorumFilmId]  INT            NOT NULL,
    PRIMARY KEY CLUSTERED ([YorumId] ASC),
    CONSTRAINT [FK_Yorum_To_Kullanıcı] FOREIGN KEY ([YorumYazanId]) REFERENCES [dbo].[Kullanıcı] ([KullaniciId]),
    CONSTRAINT [FK_Yorum_To_Film] FOREIGN KEY ([YorumFilmId]) REFERENCES [dbo].[Film] ([FilmId])
);

