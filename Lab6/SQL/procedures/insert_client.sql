CREATE OR REPLACE PROCEDURE insert_client(
        p_first_name varchar(30),
        p_last_name varchar(30),
        p_phone char(13),
        p_password varchar(20),
        p_email varchar(30)
    ) LANGUAGE plpgsql AS $$ BEGIN
INSERT INTO "client" (
        "first_name",
        "last_name",
        "phone",
        "password",
        "email"
    )
VALUES (
        p_first_name,
        p_last_name,
        p_phone,
        p_password,
        p_email
    );

END;

$$;