using System.IO;

namespace Hamster.TouchPuzzle {
    public class FieldManagerForSave : FieldManager, ISaver {

        public int GetSaveID() {
            return (int)ESaverID.FIELD_MANAGER_ID;
        }

        public void Load(BinaryReader binaryReader) {
            SetCurrentID(binaryReader.ReadInt32());
        }

        public void Save(BinaryWriter binaryWriter) {
            binaryWriter.Write(GetCurrentID());
        }
    }
}