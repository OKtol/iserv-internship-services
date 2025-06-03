CREATE TABLE IF NOT EXISTS answers (
  id uuid NOT NULL,
  application_id uuid NOT NULL,
  text text NOT NULL,
  status integer NOT NULL,
  CONSTRAINT "PK_answers" PRIMARY KEY (id),
  CONSTRAINT "FK_answers_applications_application_id" FOREIGN KEY (application_id) REFERENCES applications (id) ON DELETE CASCADE
);
