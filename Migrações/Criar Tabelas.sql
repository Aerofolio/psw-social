CREATE TABLE public.usuario (
	id uuid NOT NULL,
	nome varchar NOT NULL,
	sobrenome varchar NOT NULL,
	senha varchar NOT NULL,
	datacomemorativa date NOT NULL,
	column1 int NOT NULL,
	bio varchar NULL,
	fotodeperfil bytea NULL,
	cidade varchar NULL,
	uf int NULL,
	telefone varchar NULL,
	documento varchar NULL,
	tipo int NULL,
	CONSTRAINT usuario_pk PRIMARY KEY (id)
);

CREATE TABLE public.amizade (
	idusuario uuid NOT NULL,
	idusuarioamigo uuid NOT NULL,
	CONSTRAINT amizades_pk PRIMARY KEY (idusuario),
	CONSTRAINT amizades_usuario_fk FOREIGN KEY (idusuario) REFERENCES public.usuario(id),
	CONSTRAINT amizades_usuario_fk_1 FOREIGN KEY (idusuarioamigo) REFERENCES public.usuario(id)
);

CREATE TABLE public.notificacao (
	id uuid NOT NULL,
	idusuarioorigem uuid NOT NULL,
	idusuariodestino uuid NOT NULL,
	tipo int NOT NULL,
	mensagem varchar NOT NULL,
	dataenvio date NOT NULL,
	dataleitura date NULL,
	CONSTRAINT notificacao_pk PRIMARY KEY (id),
	CONSTRAINT notificacao_usuario_fk FOREIGN KEY (idusuarioorigem) REFERENCES public.usuario(id),
	CONSTRAINT notificacao_usuario_fk_1 FOREIGN KEY (idusuariodestino) REFERENCES public.usuario(id)
);
