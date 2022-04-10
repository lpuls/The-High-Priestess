using System;
using System.IO;

namespace Hamster.TouchPuzzle {
    public class ItemManagerForSave : ItemManager, ISaver {
        public int GetSaveID() {
            return (int)ESaverID.ITEM_MANAGER_ID;
        }

        public void Load(BinaryReader binaryReader) {
            int count = binaryReader.ReadInt32();
            for (int i = 0; i < count; i++) {
                AddItem(binaryReader.ReadInt32());
            }
        }

        public void Save(BinaryWriter binaryWriter) {
            binaryWriter.Write(_items.Count);
            for (int i = 0; i < _items.Count; i++) {
                binaryWriter.Write(_items[i]);
            }
        }
    }
}
