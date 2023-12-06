CREATE OR REPLACE PROCEDURE insert_employee(
    p_employee_role_id int,
    p_first_name varchar(30),
    p_last_name varchar(30),
    p_salary numeric(7, 2),
    p_phone char(13),
    p_position varchar(30),
    p_password varchar(20),
    p_email varchar(30)
) LANGUAGE plpgsql AS $$
BEGIN
    INSERT INTO "employee" (
        "employee_role_id",
        "first_name",
        "last_name",
        "salary",
        "phone",
        "position",
        "password",
        "email"
    )
    VALUES (
        p_employee_role_id,
        p_first_name,
        p_last_name,
        p_salary,
        p_phone,
        p_position,
        p_password,
        p_email
    );
END;
$$;