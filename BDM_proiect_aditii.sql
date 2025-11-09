CREATE OR REPLACE PROCEDURE PINSERAREVIDEOGANDAC (
    VID IN NUMBER,
    fis IN BLOB
) IS
BEGIN
    UPDATE GANDACI
    SET VIDEO_GANDAC = ORDVideo(fis, 0)
    WHERE ID = VID;

    COMMIT;
END;
/

create or replace procedure PAFISAREVIDEO(VID in NUMBER, flux out BLOB)
is
obj ORDVideo;
begin
select video_gandac into obj from gandaci where id = vid;
flux:=obj.getcontent();
end;
/

create or replace procedure PAFISAREPOSTARI(vid in number, flux out BLOB)
is
obj ORDImage;
begin
select img into obj from gandaci where id=vid;
flux:=obj.getcontent();
end;
/

CREATE OR REPLACE PROCEDURE PNUMARGANDACI(NUMAR OUT NUMBER) 
IS 
BEGIN
SELECT COUNT(*) INTO NUMAR FROM GANDACI;
END;
/


SELECT * FROM GANDACI;


CREATE SEQUENCE autoinc_seq;

CREATE OR REPLACE TRIGGER autoinc_trg 
BEFORE INSERT ON GANDACI 
FOR EACH ROW
WHEN (new.id IS NULL)
BEGIN
  SELECT autoinc_seq.NEXTVAL
  INTO   :new.id
  FROM   dual;
END;
/

describe gandaci;


