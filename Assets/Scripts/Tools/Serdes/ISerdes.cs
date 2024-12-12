namespace Tools.Serdes {
    public interface ISerdes<TF, TT> {
        TT Serialize(TF f);
        TF Deserialize(TT t);
    }
}