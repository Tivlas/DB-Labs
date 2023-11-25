DROP FUNCTION IF EXISTS get_employee_with_role;
CREATE OR REPLACE FUNCTION get_employee_with_role(IN p_email TEXT, IN p_password TEXT) RETURNS TABLE (
        employee_id INT,
        employee_role_id INT,
        first_name varchar,
        last_name varchar,
        salary NUMERIC,
        phone char,
        "position" varchar,
        PASSWORD varchar,
        email varchar,
        name varchar
    ) AS $$ BEGIN RETURN QUERY
SELECT e.employee_id,
    e.employee_role_id,
    e.first_name,
    e.last_name,
    e.salary,
    e.phone,
    e.position,
    e.password,
    e.email,
    r.name
FROM employee AS e
    INNER JOIN employee_role AS r ON e.employee_role_id = r.employee_role_id
WHERE e.email = p_email
    AND e.password = p_password;

END;

$$ LANGUAGE plpgsql;