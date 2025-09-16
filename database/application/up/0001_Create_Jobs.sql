CREATE TABLE IF NOT EXISTS jobs (
  id SERIAL NOT NULL,
  title text NOT NULL,
  is_visible boolean NOT NULL,
  CONSTRAINT "PK_jobs" PRIMARY KEY (id)
);
