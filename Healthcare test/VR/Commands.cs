using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Healthcare_test.VR
{
    public class Commands
    {
        static int terainOffSet = 0;

        public static string tree1 = Path.Combine(Directory.GetCurrentDirectory(), @"NetwerkEngineData\models\trees\fantasy\tree1.obj");
        public static string tree2 = Path.Combine(Directory.GetCurrentDirectory(), @"NetwerkEngineData\models\trees\fantasy\tree2.obj");
        public static string tree3 = Path.Combine(Directory.GetCurrentDirectory(), @"NetwerkEngineData\models\trees\fantasy\tree3.obj");
        public static string tree4 = Path.Combine(Directory.GetCurrentDirectory(), @"NetwerkEngineData\models\trees\fantasy\tree4.obj");
        public static string tree5 = Path.Combine(Directory.GetCurrentDirectory(), @"NetwerkEngineData\models\trees\fantasy\tree5.obj");
        public static string tree6 = Path.Combine(Directory.GetCurrentDirectory(), @"NetwerkEngineData\models\trees\fantasy\tree6.obj");
        public static string tree7 = Path.Combine(Directory.GetCurrentDirectory(), @"NetwerkEngineData\models\trees\fantasy\tree7.obj");
        public static string tree10 = Path.Combine(Directory.GetCurrentDirectory(), @"NetwerkEngineData\models\trees\fantasy\tree10.obj");

        public static string pony = Path.Combine(Directory.GetCurrentDirectory(), @"NetwerkEngineData\models\pony.obj");
        public static string bike = Path.Combine(Directory.GetCurrentDirectory(), @"NetwerkEngineData\models\bike\bike.fbx");
        public static string bikeAnim = Path.Combine(Directory.GetCurrentDirectory(), @"NetwerkEngineData\models\bike\bike_anim.fbx");
        public static string carcartoon = Path.Combine(Directory.GetCurrentDirectory(), @"NetwerkEngineData\models\cars\cartoon\Pony_cartoon.obj");
        public static string carcartoon2 = Path.Combine(Directory.GetCurrentDirectory(), @"NetwerkEngineData\models\cars\cartoon\Pony_cartoon2.obj");
        public static string house1 = Path.Combine(Directory.GetCurrentDirectory(), @"NetwerkEngineData\models\houses\set1\house1.obj");



        public static dynamic SessionList()
        {
            dynamic request = new
            {
                id = "session/list"
            };

            return request;
        }

        public static dynamic CreateTunnel(string clientID)
        {
            dynamic request = new
            {
                id = "tunnel/create",
                data = new
                {
                    session = clientID
                }
            };

            return request;
        }

        public static dynamic SendTunnel(string tunnel, dynamic dataToSend)
        {
            dynamic request = new
            {
                id = "tunnel/send",
                data = new
                {
                    dest = tunnel,
                    data = dataToSend
                }
            };
            return request;
        }

        public static dynamic SetTime(string tunnel, double timeToSet)
        {
            dynamic timeRequest = new
            {
                id = "scene/skybox/settime",
                data = new
                {
                    time = timeToSet
                }
            };
            return Commands.SendTunnel(tunnel, timeRequest);
        }


        public static dynamic AddObject(string tunnel, int xPos, int yPos, int zPos, int rotY, string nameNode, bool needTerrain, bool needPanel)
        {
            int[] aPosition = new int[3] { xPos, yPos, zPos };
            int[] aRotation = new int[3] { 0, rotY, 0 };
            int[] aPanelSize = new int[2] { 1, 1 };
            int[] aResolution = new int[2] { 512, 512 };
            int[] aBackground = new int[4] { 1, 1, 1, 1 };
            int[] aWaterSize = new int[2] { 20, 20 };

            if (needTerrain)
            {
                dynamic request = new
                {
                    id = "scene/node/add",
                    data = new
                    {
                        name = nameNode,
                        components = new
                        {
                            transform = new
                            {
                                position = aPosition,
                                scale = 1,
                                rotation = aRotation
                            },
                            terrain = new
                            {
                                smoothnormals = true
                            }
                        }
                    }
                };
                return Commands.SendTunnel(tunnel, request);

            }
            else if (needPanel)
            {
                dynamic request = new
                {
                    id = "scene/node/add",
                    data = new
                    {
                        name = nameNode,
                        components = new
                        {
                            transform = new
                            {
                                position = aPosition,
                                scale = 1,
                                rotation = aRotation
                            },
                            panel = new
                            {
                                size = new double[2] {0.5,0.5},
                                resolution = new int[2] {512,512},
                                background = new double[4] {1,1,1,0}
                            }
                        }
                    }
                };
                return Commands.SendTunnel(tunnel, request);
            }
            else
            {
                dynamic request = new
                {
                    id = "scene/node/add",
                    data = new
                    {
                        name = nameNode,
                        components = new
                        {
                            transform = new
                            {
                                position = aPosition,
                                scale = 1,
                                rotation = aRotation
                            },
                            model = new
                            {
                                file = bike,
                                cullbackfaces = true,
                                animated = false,
                                animation = bikeAnim,
                            }
                        }
                    }
                };
                return Commands.SendTunnel(tunnel, request);
            }


        }

        public static dynamic GetNodeByName(string tunnel, string NameToFind)
        {
            dynamic FindNode = new
            {
                id = "scene/node/find",
                data = new
                {
                    name = NameToFind
                }
            };
            return Commands.SendTunnel(tunnel, FindNode);
        }

        public static dynamic GetScene(string tunnel)
        {
            dynamic GetTerrain = new
            {
                id = "scene/get",
            };
            return Commands.SendTunnel(tunnel, GetTerrain);
        }


        public static dynamic DeleteNode(string tunnel, string uuid)
        {
            dynamic NodeToDelete = new
            {
                id = "scene/node/delete",
                data = new
                {
                    id = uuid
                }
            };
            // System.Diagnostics.Debug.WriteLine(uuid + " test " + tunnel);
            return Commands.SendTunnel(tunnel, NodeToDelete);
        }



        public static dynamic CreateGroundTerrain(string tunnel)
        {
            dynamic groundTerrain = new
            {
                id = "scene/terrain/add",
                data = new
                {
                    size = new[] { 256, 256 },
                    heights = Enumerable.Repeat(0, (256 * 256)).ToArray()
                }
            };
            return Commands.SendTunnel(tunnel, groundTerrain);
        }

        public static dynamic CreateGroundTerrainWithHeights(string tunnel)
        {

            double[] heightsGround = new double[256 * 256];
            try
            {
                heightsGround = GenerateTerrainFromPicture();
            }
            catch
            {
                for (int Terrainx = 0; Terrainx < 256; Terrainx++)
                {
                    for (int Terrainz = 0; Terrainz < 256; Terrainz++)
                    {
                        heightsGround[(Terrainx * 256) + Terrainz] = ((double)Terrainz / 8);
                    }
                }
            }
            dynamic groundTerrain = new
            {
                id = "scene/terrain/add",
                data = new
                {
                    size = new[] { 256, 256 },
                    heights = heightsGround
                }
            };
            return Commands.SendTunnel(tunnel, groundTerrain);
        }

        public static dynamic addTextureTerrain(string tunnel, string uuid, string normal, double min, double max, int fade)
        {
            Boolean FilesOnThisPc = true;
            if (FilesOnThisPc)
            {
                dynamic Texture = new
                {
                    id = "scene/node/addlayer",
                    data = new
                    {
                        id = uuid,
                        diffuse = Path.Combine(Directory.GetCurrentDirectory(), @"NetwerkEngineData\textures\" + normal),
                        normal = Path.Combine(Directory.GetCurrentDirectory(), @"NetwerkEngineData\textures\" + normal),
                        minHeight = min,
                        maxHeight = max,
                        fadeDist = fade

                    }
                };
                return Commands.SendTunnel(tunnel, Texture);
            }
            else
            {
                dynamic Texture = new
                {
                    id = "scene/node/addlayer",
                    data = new
                    {
                        id = uuid,
                        diffuse = Path.Combine(Directory.GetCurrentDirectory(), @"NetwerkEngineData\textures\" + normal),
                        normal = Path.Combine(Directory.GetCurrentDirectory(), @"NetwerkEngineData\textures\" + normal),
                        //diffuse = @"C:\Users\Aaron\Desktop\NetworkEngine\data\NetworkEngine\textures\terrain\" + normal,
                        //normal = @"C:\Users\Aaron\Desktop\NetworkEngine\data\NetworkEngine\textures\terrain\" + normal,
                        minHeight = min,
                        maxHeight = max,
                        fadeDist = fade

                    }
                };
                return Commands.SendTunnel(tunnel, Texture);
            }


        }
        public static dynamic SwapPanel(string tunnel, string uuidPanel)
        {
            dynamic swapPanel = new
            {
                id = "scene/panel/swap",
                data = new
                {
                    id = uuidPanel,
                   
                }
            };
            return Commands.SendTunnel(tunnel, swapPanel);
        }

        public static dynamic clearPanel(string tunnel, string uuidPanel)
        {
            dynamic ClearPanel = new
            {
                id = "scene/panel/clear",
                data = new
                {
                    id = uuidPanel,

                }
            };
            return Commands.SendTunnel(tunnel, ClearPanel);
        }

        public static dynamic addTextPanel(string tunnel, string uuidPanel, string Text)
        {
            dynamic TextOnPanel = new
            {
                id = "scene/panel/drawtext",
                data = new
                {
                    id = uuidPanel,
                    text = Text,
                    position = new int[2] { 100, 100 },
                    size = 128,
                    color = new int[4] { 1, 1, 1, 1 },
                    font = "segoeui"
                }
            };
            return Commands.SendTunnel(tunnel, TextOnPanel);
        }

        public static dynamic UpdateNode(string tunnel, string uuid, double transformY, double transformZ, int RotateY, int RotateX)
        {
            dynamic Update = new
            {
                id = "scene/node/update",
                data = new
                {
                    id = uuid,
                    transform = new
                    {
                        position = new double[3] { 0, transformY, transformZ },
                        scale = 1,
                        rotation = new double[3] { RotateX, RotateY, 0 }

                    }

                }
            };
            return Commands.SendTunnel(tunnel, Update);

        }

        public static dynamic UpdateNodeWithParent(string tunnel, string uuid, string parentID)
        {
            dynamic Update = new
            {
                id = "scene/node/update",
                data = new
                {
                    id = uuid,
                    parent = parentID,
                    transform = new
                    {
                        position = new double[3] { 0, 0, 0 },
                        scale = 1,
                        rotation = new int[3] { 0, 0, 0 },

                    }
                }
            };
            return Commands.SendTunnel(tunnel, Update);

        }

        public static dynamic addSkyBox(string tunnel)
        {
            dynamic skybox = new
            {
                id = "scene/skybox/update",
                data = new
                {
                    type = "static",
                    files = new
                    {
                        xpos = Path.Combine(Directory.GetCurrentDirectory(), @"NetwerkEngineData\textures\SkyBoxes\interstellar\interstellar_rt.png"),
                        xneg = Path.Combine(Directory.GetCurrentDirectory(), @"NetwerkEngineData\textures\SkyBoxes\interstellar\interstellar_lf.png"),
                        ypos = Path.Combine(Directory.GetCurrentDirectory(), @"NetwerkEngineData\textures\SkyBoxes\interstellar\interstellar_up.png"),
                        yneg = Path.Combine(Directory.GetCurrentDirectory(), @"NetwerkEngineData\textures\SkyBoxes\interstellar\interstellar_dn.png"),
                        zpos = Path.Combine(Directory.GetCurrentDirectory(), @"NetwerkEngineData\textures\SkyBoxes\interstellar\interstellar_bk.png"),
                        zneg = Path.Combine(Directory.GetCurrentDirectory(), @"NetwerkEngineData\textures\SkyBoxes\interstellar\interstellar_ft.png")


                    }
                }
            };
            return Commands.SendTunnel(tunnel, skybox);

        }

        public static dynamic ResetScene(String tunnel)
        {
            dynamic sceneReset = new
            {
                id = "scene/reset"
            };
            return Commands.SendTunnel(tunnel, sceneReset);
        }


        public static dynamic AddRoute(string tunnel)
        {
            int[] pos1 = new int[3] { 0, terainOffSet, 0 };
            int[] pos2 = new int[3] { 30, terainOffSet, 30 };
            int[] pos3 = new int[3] { 105, terainOffSet, 60 };
            int[] pos4 = new int[3] { 120, terainOffSet, 15 };
            int[] pos5 = new int[3] { 115, terainOffSet, -40 };
            int[] pos6 = new int[3] { 15, terainOffSet, -65 };
            int[] pos7 = new int[3] { -30, terainOffSet, -85 };
            int[] pos8 = new int[3] { -60, terainOffSet, -60 };
            int[] pos9 = new int[3] { -75, terainOffSet, 65 };
            int[] pos10 = new int[3] { -60, terainOffSet, 80 };
            int[] pos11 = new int[3] { -20, terainOffSet, 40 };



            int[] dir1 = new int[3] { 5, terainOffSet, -5 };
            int[] dir2 = new int[3] { 5, terainOffSet, 5 };
            int[] dir3 = new int[3] { 5, terainOffSet, 5 };
            int[] dir4 = new int[3] { -5, terainOffSet, -5 };
            int[] dir5 = new int[3] { -5, terainOffSet, -5 };
            int[] dir6 = new int[3] { 0, terainOffSet, 0 };
            int[] dir7 = new int[3] { -5, terainOffSet, -5 };
            int[] dir8 = new int[3] { 0, terainOffSet, 0 };
            int[] dir9 = new int[3] { 0, terainOffSet, 0 };
            int[] dir10 = new int[3] { 5, terainOffSet, 5 };
            int[] dir11 = new int[3] { 0, terainOffSet, 0 };
            dynamic node1 = new
            {
                pos = pos1,
                dir = dir1
            };
            dynamic node2 = new
            {
                pos = pos2,
                dir = dir2
            };
            dynamic node3 = new
            {
                pos = pos3,
                dir = dir3
            };
            dynamic node4 = new
            {
                pos = pos4,
                dir = dir4
            };
            dynamic node5 = new
            {
                pos = pos5,
                dir = dir5
            };
            dynamic node6 = new
            {
                pos = pos6,
                dir = dir6
            };
            dynamic node7 = new
            {
                pos = pos7,
                dir = dir7
            };
            dynamic node8 = new
            {
                pos = pos8,
                dir = dir8
            };
            dynamic node9 = new
            {
                pos = pos9,
                dir = dir9
            };
            dynamic node10 = new
            {
                pos = pos10,
                dir = dir10
            };
            dynamic node11 = new
            {
                pos = pos11,
                dir = dir11
            };

            dynamic[] routeNodes = new dynamic[11] { node1, node2, node3, node4, node5, node6, node7, node8, node9, node10, node11 };

            dynamic request = new
            {
                id = "route/add",
                data = new
                {
                    nodes = routeNodes
                }
            };

            return Commands.SendTunnel(tunnel, request);
        }

        public static dynamic AddRoad(string tunnel, string uuid)
        {
            dynamic request = new
            {
                id = "scene/road/add",
                data = new
                {
                    route = uuid,
                    heightoffset = 0.01
                }
            };
            return Commands.SendTunnel(tunnel, request);
        }

        public static dynamic UpdateSpeed(String tunnel, String uuid, int speed)
        {
            dynamic Speed = new
            {
                id = "route/follow/speed",
                data = new
                {
                    node = uuid,
                    speed = speed

                }
            };
            return Commands.SendTunnel(tunnel, Speed);
        }

        public static dynamic MoveObject(String tunnel, String id, String road)
        {
            dynamic moveObject = new
            {
                id = "route/follow",
                data = new
                {
                    route = road,
                    node = id,
                    speed = 1.0,
                    offset = 0.0,
                    rotate = "XYZ",
                    followHeight = false,
                    rotateOffset = new double[] { 0, 0, 0 },
                    positionOffset = new double[] { 0, 0, 0 }
                }
            };
            return Commands.SendTunnel(tunnel, moveObject);
        }

        public static double[] GenerateTerrainFromPicture()
        {
            Bitmap terrainBitmap = (Bitmap)Bitmap.FromFile(Path.Combine(Directory.GetCurrentDirectory(), "HeightmapBW4.jpg"));
            double[] toReturn = new double[terrainBitmap.Width * terrainBitmap.Height];
            for (int x = 0; x < terrainBitmap.Width; x++)
            {
                for (int y = 0; y < terrainBitmap.Height; y++)
                {
                    double r = Convert.ToDouble(terrainBitmap.GetPixel(x, y).R);
                    double g = Convert.ToDouble(terrainBitmap.GetPixel(x, y).G);
                    double b = Convert.ToDouble(terrainBitmap.GetPixel(x, y).B);
                    toReturn[(x * 256) + y] = ((768 - (r + g + b)) / 15);
                }
            }
            return toReturn;
        }

    }
}
