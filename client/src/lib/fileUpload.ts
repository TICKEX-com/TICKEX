import { ref, uploadBytes, getDownloadURL } from 'firebase/storage';
import { storage } from "@/lib/firebase";
import { v4 } from 'uuid';

export const uploadFile = (fileUpload: FileList | null, setFileUrl: (url: string) => void) => {
  if (!fileUpload || fileUpload.length === 0) return;
  const file = fileUpload[0];
  const fileType = file.type.split('/')[0];
  const uniqueFileName = `${fileType}s/${file.name}-${v4()}`;
  const fileRef = ref(storage, uniqueFileName);

  uploadBytes(fileRef, file).then((snapshot) => {
    getDownloadURL(snapshot.ref).then((url) => {
      setFileUrl(url);
      console.log(url);
    });
  });
};