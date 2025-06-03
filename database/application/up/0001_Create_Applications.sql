CREATE TABLE IF NOT EXISTS applications (
  id uuid NOT NULL,
  user_uid uuid NOT NULL,
  first_name text NOT NULL,
  last_name text NOT NULL,
  email text NOT NULL,
  phone_number text NOT NULL,
  about_me text NOT NULL,
  status integer NOT NULL,
  CONSTRAINT "PK_applications" PRIMARY KEY (id)
);
