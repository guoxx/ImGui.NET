namespace ImGuiNET
{
    public unsafe partial struct ImDrawDataPtr
    {
        public RangePtrAccessor<ImDrawListPtr> CmdListsRange
        {
            get { return new RangePtrAccessor<ImDrawListPtr>(CmdLists.ToPointer(), CmdListsCount); }
        }
    }
}
