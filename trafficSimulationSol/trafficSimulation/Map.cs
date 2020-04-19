using Microsoft.Xna.Framework;
using System.Runtime.Serialization;

[DataContract]
public partial class Map
{
    [DataMember]
    public int Id;

    [DataMember]
    public int Row { get; set; }

    [DataMember]
    public int Column { get; set; }

    [DataMember]
    public Rectangle SquareDestination { get; set; }

    [DataMember]
    public Point SquareCoordinate { get; set; }
}
