using System.IO;

namespace Hamster.TouchPuzzle {
    public class BlackboardForSave : Blackboard, ISaver {

        public int GetSaveID() {
            return (int)ESaverID.BLOACK_BOARD_ID;
        }

        public void Load(BinaryReader binaryReader) {
            int count = binaryReader.ReadInt32();
            for (int i = 0; i < count; i++) {
                int key = binaryReader.ReadInt32();
                int value = binaryReader.ReadInt32();
                _data.Add(key, value);
            }
        }

        public void Reset() {
            Clean();
        }

        public void Save(BinaryWriter binaryWriter) {
            binaryWriter.Write(_data.Count);

            var it = _data.GetEnumerator();
            while (it.MoveNext()) {
                int key = it.Current.Key;
                int value = it.Current.Value;
                binaryWriter.Write(key);
                binaryWriter.Write(value);
            }
        }
    }
}
