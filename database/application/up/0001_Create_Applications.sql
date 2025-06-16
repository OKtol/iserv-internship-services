CREATE TABLE IF NOT EXISTS applications (
  id uuid NOT NULL,
  user_uid uuid NOT NULL,
  job_id integer NOT NULL,
  first_name text NOT NULL,
  last_name text NOT NULL,
  email text NOT NULL,
  phone_number text NOT NULL,
  about_me text NOT NULL,
  status integer NOT NULL,
  solution text NOT NULL,
  verification_status integer NOT NULL,
  answer text NOT NULL,
  solution_status integer NOT NULL,
  test_task text NOT NULL,
  correct_answer text NOT NULL,
  CONSTRAINT "PK_applications" PRIMARY KEY (id),
  CONSTRAINT "FK_applications_jobs_job_id" FOREIGN KEY (job_id) REFERENCES jobs (id) ON DELETE RESTRICT
);
