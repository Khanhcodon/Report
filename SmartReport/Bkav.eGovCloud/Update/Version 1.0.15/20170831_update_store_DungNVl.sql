UPDATE store
SET UserViewIds = REPLACE(UserViewIds, ',', ';');

UPDATE store
SET UserViewIds = REPLACE(UserViewIds, '[', ';');

UPDATE store
SET UserViewIds = REPLACE(UserViewIds, ']', ';');