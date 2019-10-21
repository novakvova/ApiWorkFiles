﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebApiFiles.BLL.Interfaces;

namespace WebApiFiles.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        private readonly IImageWorker _imageWorker;
        public ValuesController(IImageWorker imageWorker)
        {
            _imageWorker = imageWorker;
        }
        // GET api/values
        [HttpGet]
        public ActionResult<IEnumerable<string>> Get()
        {
            string base64 = "data:image/jpeg;base64,/9j/4AAQSkZJRgABAQAAAQABAAD/data:image/jpeg;base64,/9j/4AAQSkZJRgABAQAAAQABAAD/2wCEAAkGBxASEhUSEBIVFRUXFhcXFRgVFRUVFRUVFRUWFxUWFRUYHSggGBolHhUVITEhJSkrLi4uFx8zODMtNygtLisBCgoKDg0OGxAQGi0lICUtLS0tLS0tLS0tKy0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLf/AABEIALcBFAMBIgACEQEDEQH/xAAcAAABBQEBAQAAAAAAAAAAAAACAQMEBQYABwj/xAA6EAABAwIEAwYEBQMEAwEAAAABAAIRAyEEBRIxQVFhBhMicYGRMqGx8EJSwdHhI2JyBxSCknOi8ST/xAAYAQADAQEAAAAAAAAAAAAAAAAAAgMBBP/EACURAAICAgMAAgEFAQAAAAAAAAABAhEDIRIxQSJRYRMyQnGxBP/aAAwDAQACEQMRAD8A9UCMBIEQQaKEQXAIgEAclShKAgBEQXAJUAclXKPiMSG2F3cv3WN0aPkpqrWaNyo4e47n0Cr8xqxtA6pJTpWMo2FmGNL/AANNtz5KgzXF92NLA01S3V4vgpM/PU/QcSOV09iMVpbIEuNgPWGt6SSBPUngmMDlBe/xnVLg+o7bW5uw6MEC39omy57t2yqVIxOLyluh+IxLjpnUTU+Oq/gXtj2pgQL2m488zPE964n4QbNDvijhDRwtH6r0j/UjEFzobZjPDSbFi47vI42v9OKyfZ/Jm1KxdBc6RPIl0BrRfadMne/AKmN6thNN6RO7Mdmm0qXfvp95Wdai18aWmJNRwuPCD1uRF9kx9ZjCS5wfHx1KpkCfytvedgJJiep9PzbKRRowDBDdIdyaPidtaSSV49isH3znP1BtBhMOOx4E8pPn04JU3KTbNqloSqe8FiYAIJJ628EWPqTHRRMLlrNUE24SCJPXntw91YHF0mgMpeA8CfCTziQSfYHqpOFw5qAy6HD82zoOxO7dpm6omZRKy3U1jmlsngW2BvsBflxupeAxz2aqb9nbzwi40g7EWM9OOyHD4N/MggjxNMlj4AuJ2cAB6QrJ1BrgHuaDw3PxDcQdh0O3W6Vs1I9M7N4wvoMJnbjyGxKuw5eZdks4NJ+kuLqbzEndruNuA4R5L0WlUkWVYStEZxpkpcha5GnEBKSESQoAFCQjSFADZCEpwoSgBooSE4UBCAGyEiIpUAPBGEIRhAChEkRBBpwRJEqAFSpEqAGsRUIED4jYfv5KK5gbYGSbuPFO1XiXOPCw+p/RQqDi4ydvmVKUtjJD1WoGi26zOZ4qXaRfi7y4DzJi3KVe44mLCVlcXUAd3bLvcbu3Am09TaFz5ZMrBEnLqOpxe42adzzjxH5ke6sP914SGi7oaBxvA/UKjzTGNptZQbuQPPSOPnufUqTk1bVWYPygu8otB9S0+ySMvB3H0idpMlafGROgcRMwZc6OMu+o6qL2OyltF7S6C/U6o87+LYX6Ax5iVp83qQwmYDQJPlf1O/qeizjHupMc59nVDMflZwHt8yjJPhHRsIuTLHtTiRWApaoa4+P/ABF4PSLrzbtFW1v7prIa2zWi0HmQLF3M87DZanCVi5tTEPPMM6xu73LWj1Ubs7khrONUjc7cN0Rm0tlVBGXo9lKpbq0yd4J3HMffJabLML3dIamSBz+Jvv8AZXoNLL2hoEKnzLLyCSzimU36K4rwo8B3bjFiIgHkB+F3Lz5J6vDdTIEOBNwIPXbe8eXPjWPJa/U3wOBuLwSDsRwmVz8xDneIaXcWT8Q5sP4revBNaEcWV9bEmlUl+rSbGNrTeOBH2F6f2YzIVKYvMD/4V5diaonTUjSYg3seB6cP1kbWPZHM3YeuKL9jdl7Oabw3zmR1TxdOxJxtHsLCngoGEramggyCJHkVNYV0I5w0iVItARIURQlAAlCQjKFADZCApwoCgwBclK5ADwRhAEYQAQShIESDRQlSJUAKFxKVNYh1ljAqMS8uOnhJJ905RrBsDidgufSiev03TeEoQS9252H5RwXKrstqhnPap0adug/UrJZNhjLq7pIExxm5HhHGNuU8yDOgzImq7Q0Twd5cfdRc6xdHDUi0EEj4jtHJrR5QAOvOVOW22PHVIzGZ4jutVZ96j5FNu+mBN+em09dPVWfYak+C9/xP0jjZovfqTw/tWRwdGrjcVqfOnVppt4Nay5c5vACQf7i6ORXqeQ0mgwxvw/L+VlVUfsdvVh5uxlKnqqHr1JOwjmeS8/zLEOrPImBx/taef9xv9gLVdqZe7+o7bYbnlYbD5ys0ylqMNG5MDe5tvx6np5KOSXKX9FcUaRIpYQ1GsosEARPk3Ye5cfMhbfKsvbSYGgRAUPJct7tondX1Nll1Yoatk8k/EM1RZQa1OVZPYoz2onEyMjBdscuMd9Ts5vxRxbxtsf4WWGYU3tioLiLjn5Hj09l6jmlEFpXmGeZZ3TyQPAZi239p5j6Ka+mV72iacMK1ItYZIEtJuBwLXDlfyTWW4LvB3ZEOZ8PMOm4E7cL3Hms4yq+k/VScWncBxJaf8XTb5rSMr69NQPDao8QJjS6PiaZ4ETccpvCba0Ta9PQuyeYlzdFSzxY7/F689/PzC1dIrF5PWbVAeBpfEG9+cOPEixH8rX4WpIE78fPiujFK0cuRUyUuXArlYQRIlSFAAlCUZQlAAFAU4UBQYAuSpEAPNRhC1EEAEEqQJUGihEEiVACpmqPv1TyZxGyxgRKokkeX38lDq4mfC3l7eaOrX0uqTyH6qLTH9Mni76LllL6/JZIpsfjSwENsPxOiTM/O+w8uay9VtTF1e7pi3F14Y2Tx/Mb3nfbiVd9oqbmNaxolz4AF4ksbuR+G0FSezmGaGxuybmL1XCxMfl+W/mYw72WfWiRlWVUqDC2kPwkaucTOj+2Sb2m55KywOILaXghrfzc+o/dTsPS1PB+/uwWbzmliBWfSJDaA0mmGWJbu4vPF0+EN2tPG1EtuQl38SszA63FrQSCZJm7jtJcen7W43WS5YGw4gTFuQ8vksziKWLmWMIbwu30HlxniY4WbdZNjMS1wFZvh2mRbrHJSUUnbOjuNI2FBoCOtiGsEuMBN0nyLKuzsamaSCZ9N1086Wjn4W9lLnfa54caeGpy7mQbHy++PSavD5XjsSNWIrvpg8zpPo2nBHqfQqTmWBxRDKeF0sfUeG6tIOlu9R8HcNBFzMlzRxkY3HZPmDsT3DsTi+MkVNDSZbpa0NdufHPhAAAMkGzwjKaszJkjB0bml2dZTFsViJ/8AJLf+jpCHE5cKjC03/dZCj2bzCniRRZjqtShPiOqagby1OnSeEjZenYXCBjAAIgR7KOSJWM2ecjs0Ayo/g0uLhwFgJjyJVLhapYS07GSL3A3sefUdFts3xfdsr0iCdTpj8zS1vh8iZb6rLPpd7XcALNhjTBAcWBzXO/5ODvSFNbH7Cy3PP9u8VC6aTjpLrnS8GDqH4WyfQm1iJ9ZybHNqsDmkEG9vqF4Dn+Kdg8TpAL6bmNLm8PFMgA2kX9yNiVtewmZPp1GNpkuovjTuWjmI3aROyuvi7OecbtHr7HJxR6bk+0rpOcUoUSEoAQoSiKQoAAoCjKEoMAKRKVyAHmowgajCAFCVIEqDQglSBKgBUxiPonkxiTA+/VLLoF2UOPMl56sHzITmCGqOQaPnBPyTWYO00i7i46h5cPr805gjpknYj5mf00rij+46X+0DO8M14Bi7QRPLgfkVQUarjVpsbIaXRA5NvHl/Cue9LnVG82ah56hP6qDg60VGtECC3Va5bqg35WPuEknylY8dI0WWuID9VoMD/GLffRZTtnjXim6owFxp3LWxLm8Ynbn6FW2YYhzNQG8gecS2/TcqnfWD9L92usfI8/mCmyzpJGY47swmZ0szexlTvakVGSxlFwYA4h0NL7aoOgnjEwvQOy2S16dNuutUq6rlr5cWXMeI7iIkEyPkuyTKtJ3eQDIl508dmz1+S19BkBdClGcaSFcZRlbYzhmaHaeH0SZgwFwnZHWN5Q4y7ZSN0mhkraYb6NN0OjxAQLkACZO3X6BQ8Xl3eCDP/Y/fBP4WrKntVIvkhGnEqcNgG09gJR1nWUvFOVZWcpTdOkWgrVspcyw1IvBqbG3zkfP6pnE9mg2KtJwc0mYuIMWBP/xMdpsUGs1OIDQQDNrEx+qOvndLDYU1DUEBvhbI8Rjwtb1NtkkUro18u0efducucaz5afCKYaSInS2XEdPGfZP9jKVWk4taYvbV8MiSD0tI9Vo8hzUY+j3ddrWvcCWPb+F8TsZtsPux0MDHDSQYPKQSLJ3J1RPvZuckzbvAG1AWv5G4MbwRur1hWYyaiyzovvfh0WjpOXTBto5ZKmPriuBXJzASkKIoSgASgKMoHIMAK5KkQA61GEDUYQAQSpAlQaEEqFEsA5QcwqD4ffyUuq+BKqjTc8yLDif0HNSyy/ih4L1kHGUTWOk7GLcmi/8AHquzcFopxtrBPsR+vyVq6k1jbevMnqq7MAHscJuDvysP3UJx4p32VjK3+CtoYgNrgnY2P/LUmH0iKvMTHly/n+FBxTyXw2ZOwG8Cw8tz7q4wj9Lqki5DXe4/cH3XPB62WkqG8a8knmPrZUmBdaoPw96QPPVw6furfMKjWhxe6L3P36LK1O0VIVG0QI30D/EAuvxd4p90sk5JpFMdJ7N7lThZXgNllsjrTBWkY9dOGWhM8fkMYrgE7pGm5Vfn+Eq1Gf0XaX3E2tPETaVTYXLMy0w+rTN9yCbdWggH5Jm6b0Yopx7J9TFsa+GuBcIkAiYPMK+1Kiyns1Rw9xLnF2t7ju9/5nHirlzlsdCzafQ3XKqcU5T61RQKgkpHtjJ0jEf6j2wbjvNSkI5gvEj1C85xVIN7pwgw1ovF4A0m+xAMR5r0X/VTEtZh6TD+OuweUAmfospkuV98wMJDWtOovd8LBqhwIG5kEgbkkDiFaGkSltmo7G0202GqRANmWjfxGPKw91budcCSfC254mx/U+ygPfTqRRpS1tKNIO7wfzHnckjmEuExc13U4+E7/wBo2d5GyjJDo1uXvj781oMO6yzFB97cT9IWgwT7LpxvRzT7LJpRJthRqohxQlKkKABKByMoHIMAK5cuQA6EYTYKMIAIIkIRBYaKuJXBN1TwQwAcNZjhxTlRsCyWmLJqrV4JOlY3ZFxBJPkqrFUuBmJki1z1Vo+pvHuqzFPbsDfouTLsvAZoUQD/ABJ9YUermLGlxbBixcbkxvHAASq7OccKZLJ1ECXifCwdY3Pv7rMZziy6nJM0yYIbe55gb9R9bzDk3ouoekHth2lLgRTPhB9xzPncjoGn8QC89x2NqNqMeCSWHckmXTqcfXVHkAFcZp4g6bXl0mbAiTHP5R5BNU8v1M16Zgk/9QAdt7An0XZiSiiORNnrnY7HB7GOb8LmhzfI8PRbanVXlfYbMKTQymDAddgkWdckD2JjzXo1CpIiVFri6LcuSssH1wN1X1s/Y21j9UxmuWd6J1vEbhjtM/JVdLA5dTH9RgHPvC5xP+WuZW8n9lsWODVtWP4jtYzVEku/Ixpe4/8AECU5hs6rPdp/29Ro4ucAwD0cZPpKGlm+Ha3u8LTn/BkNHrspuEpuPidv9OiG/wAjTUEuq/0kSo7zCfq2CzfafN+4ove2NYY4tB5gTJHIWQjlPMf9Vs37/FtoMNqIvy7x13SeQAHzTmXY5rmMY13hjUZkFzyILj0gtgcPVYc1XO1SZe9xLidzJn5m58gtFkrDpHC9jeRF59JC6JpKJDHbk2bTAveHgm+/ASQNRbeOoV4aIAaQz+pUhrj+RgGqPU3VZkzi0tLhPPjFjB91dYalU0Om4Ozt5k8T97LkcjoocoOdrBbz2W0wDpAWMp1gHBvIX81rcqNlf/nv058xbsTibYjXUQOKEpSkKABKFxRFA5aYCuXJEAOBGE2EYQAYRBAiWGhSgA/dKV3NACuMD0VZJJH9x+Q+5U3FvhqjUx4h0b9T+ylPbSHjpWDiIFlTYmpoa5ws64afyk8fvkrbGH6qmr1b2Ew0nnJiQB6gewXNndMtjKbDZHrBdWfpEAlp3ncF7tyenXgs/m9GgDpDi+JPhhoAHHiT6zutTqaToqPhu7nT4S7czzH3yWb7RV6YtScI2HdgzvfxHh6rlVKjqVsw+bBrdmlmqw1k6jM+sWI2n2MX3Z6iw4Qh4uX6P/USR6FQcv7PmrV1OEMAPigGAbnf8TjboFcYhzWN0Mpy25lxAEm3muznGKpEHFydszmCwtWpS1MguDxpLbOMGWmR+KD8ls+zvbAazQxJ01Gu0ySNL4tvwd0WQp55/tgX0GtkTAjwsJ/FpnYRbyCqn1mPJcfxEkzxJ3LT16qkvmrFj8XR9C4bEBwBBUpuDpuu4A+a8W7NdoMRRLWNdrafhD5vyAd+E22ut9l/bjD/AA1ppO5PsJ/y2PuorT2WcW1o2Yw9MbAJquAAqR/arCRPfs/7BUOc9t6UaaMvPSw9/wBpTylHwSMJelxnOaNptJPtzXnOc4p9WhiKjtyxwHICDYKVWxT6t3GSfYeSdxeB/wDzuZ+YEf8AqVKyvGkeXYDBmoRp34rW5E3ZpG2xP69ZUDIcIWAE7larB02ggxv9eB81TLO9E4RpFzgKLmiOYv8AwptKq6n8JIPQ38kFKpYRuVZYHKy67lOEG3Zk5pA4Cial3NAM7gRPmFq8vpQITGEwUBWdJkLshGjknKx5iNCESoKIUJSlCUAIUBREoCtMEXISlQAYRhNhGCgBwJQgCILDQkqFKgBjE7O6D+f2QMIn0j2KeHHzP7foo7LOI++SRrYy6I+YGRHE/QffzVa1rdcvs0WA/MT9/JWVc+P0I+kquxrIc0+fvYj6LlzLfItj+hnNcOHXHo0be6yGbU3AEiIHCB7CVrq1QtpAjdwd8W25F48lkWvccRT78AtlvDw6i4RPu35ny4pxuWjqxypDuNqFlIMFnBoJ1WAc5sgHoBc/yqGrhnNaCXHbU57jeXQZHIxA6cNlqO09AGuBfSdZdyI16APZnzUTFZcamhsW+Nw/C4/ha7oDw6DgrpU6Fu1Zg8yy5pIfL9pJLQS8HZ2kG09d480xgctbUf3ZBAMb7m/PYEytTm2BLiY2aJnYn+4u4C1mgbDeIVPl2Eio0NafETMWgFpaC50yTc7/AKK6yaE4ejOFpTWII0sMAQDaNiCTYi11s8JgGYhpZVAL2iCYHi/K4efyMrP5lhA1z3wYB0gCBIbFm9S4knyWnyeu06HtnULOBEEzvAI8lKbvZaPRSYnscWk6dl1Ds64br0hlJrmgi8pt2DHJbTM/UMtluUQZKLMWeIN5ArWMwkBZnEN1VH9LLGjFKzFDCODyyOJg9FZsZLSJiLA9VoW5c124UfE9mn6NNJ1pkzvve6Lszos+zuE1gOcFsMPhwAqjs5RDGhrrEc1pWMXZiS4nHkb5AMppwBFpSqpMRcuKQoAQoSlKElAAlCSlcUBK0wRKglcgwcBRApsFECg0dBRgpoFGCgA5SyglKsNOb9+qjubLinnvACZA578uEpWCIgMuM8/fe46IMVR1C/vyPAp3F1AOpUJ9R54qE5Jaey8It7RFzaj/AEBJAIJEkwN3ETymVnhlVWNRr0uElr3AAAyLagL/AHMrQ1DpaWvuH8hOmNnRxM/RZ3F9nq9QQ52pvnM8fhtH3uuKStnVDWmx/F4hrhLajXloHwnhznjPT5qJiMYGvk7Gm0neI8Wr30n5JzK+z3cE66ljNnETfeen7KvzXCmq52iQWyG9RF/Ii5jkTulSo3RZYKrSrMdqIbTbJeTbU6+o+VoHKJ5KgzHM6DCO6pTBEF0T08OzOB4m94TGXhwoCm4Q4PHeDhsCD1GoA9VoMH2WpVIJcWnh8Jnr4h6+qrGvTHoqc4rCrTadNpa8xezhDr8w6OP4gjwOasAAqENBmA4x7P8A35bLQjs26lID9U3EgR1mBsVAqZMQ4mCN5At9+dwUP6Gi0XuV45hHhd5jY+cK6peK6xNHB6HAhsRysSIWtyisCqY5bonkjq0WL2eErJ4Sjqe939zvqtfX2Pks9llPwE/3O+pVJrZOL0BTpQVPohM1I9UNOqRsCp0kNtk9zAmHYgj4XH0KQlzviMDkP3QB7BYJmYOtxlfg4+sJ1uZ1hvB9FwFkzUatuS9FqL8JAzt3Fg90/SzqmfiBb8wql9NRqjVqyTQfpwZrWVA4S0gjouJWOoY99F0tNuI4FanCYttRoc3Yq+PIpEJwcR5xTZKVxQEqpM6UiAlKgwMFGCmGuRgoAeBRApoFECg0eBXSmwUUoA7fdNY2uGNnidk9KoMXie8fbYWH7+v7KWSXFFMceTDbJublPMpIKAU6kxc0YWdMpURXYYSmK2D5GFcFgUSuU8sSEWRmeqZdTDpcxp6kTf1UmrSYWgadtiLGBsDz+/JFi3JltRR4pFHK+yLUwVOCdIcDIJHM3Mjdpm/r1TuGrMYC0ut1/VBjsvL/AB0nupviNTdyOAdzHmqipha48NfxDbUwBrh1jY+XFScWuh1TGc6xT9X9Kq6J2aXEeolSOztZwGmo7UHCRMm83ieCpP8AZE1PiMC07SOH62PLyV1QZ4qcbaBI9T+yyCHk9UXLmh4+fryR4RxY4JvCgyfP9ApTaasoici7bVDm+ircHT0tjqfmU7SMBcDCr2SWgTSbMwJ58UFR4CStUUCuSUdG9jeMx52bcqTlWEPx1Nzw5IMBggDqO6sjZCXrFcvEOlIWpoVF3epkIznsUatTUnUmazrIaQJlLjU52azHS80ybG481HzN6osPitFVruqWOpDyXKJ6lrlA4qJga+poT7nLsOMUuXJouSoMFa9ONcuXIAMORhy5cg0IFEHJVyAIuZ19FJxG+w9bKhwxXLly538kdWFfEs6BU+m5cuWwMmc+ootdy5cmkYinxj0w1y5coMqiyw5snKjRF9oPyC5ch9GGXzSkKYtu4z5WP36JMnvE8BHsuXKSVMr4XVID3/iP0UumFy5ViIx4FA9y5cnFQy9NEJFyU0fovhOveuXJk9CtEV9VCyukXJGbRIa9M1nrlyexShzR1iszXfBlcuSjo33ZzEaqbfIK4c9cuXYjiYyXrly5aKf/2Q==/AABEIALcBFAMBIgACEQEDEQH/xAAcAAABBQEBAQAAAAAAAAAAAAACAQMEBQYABwj/xAA6EAABAwIEAwYEBQMEAwEAAAABAAIRAyEEBRIxQVFhBhMicYGRMqGx8EJSwdHhI2JyBxSCknOi8ST/xAAYAQADAQEAAAAAAAAAAAAAAAAAAgMBBP/EACURAAICAgMAAgEFAQAAAAAAAAABAhEDIRIxQSJRYRMyQnGxBP/aAAwDAQACEQMRAD8A9UCMBIEQQaKEQXAIgEAclShKAgBEQXAJUAclXKPiMSG2F3cv3WN0aPkpqrWaNyo4e47n0Cr8xqxtA6pJTpWMo2FmGNL/AANNtz5KgzXF92NLA01S3V4vgpM/PU/QcSOV09iMVpbIEuNgPWGt6SSBPUngmMDlBe/xnVLg+o7bW5uw6MEC39omy57t2yqVIxOLyluh+IxLjpnUTU+Oq/gXtj2pgQL2m488zPE964n4QbNDvijhDRwtH6r0j/UjEFzobZjPDSbFi47vI42v9OKyfZ/Jm1KxdBc6RPIl0BrRfadMne/AKmN6thNN6RO7Mdmm0qXfvp95Wdai18aWmJNRwuPCD1uRF9kx9ZjCS5wfHx1KpkCfytvedgJJiep9PzbKRRowDBDdIdyaPidtaSSV49isH3znP1BtBhMOOx4E8pPn04JU3KTbNqloSqe8FiYAIJJ628EWPqTHRRMLlrNUE24SCJPXntw91YHF0mgMpeA8CfCTziQSfYHqpOFw5qAy6HD82zoOxO7dpm6omZRKy3U1jmlsngW2BvsBflxupeAxz2aqb9nbzwi40g7EWM9OOyHD4N/MggjxNMlj4AuJ2cAB6QrJ1BrgHuaDw3PxDcQdh0O3W6Vs1I9M7N4wvoMJnbjyGxKuw5eZdks4NJ+kuLqbzEndruNuA4R5L0WlUkWVYStEZxpkpcha5GnEBKSESQoAFCQjSFADZCEpwoSgBooSE4UBCAGyEiIpUAPBGEIRhAChEkRBBpwRJEqAFSpEqAGsRUIED4jYfv5KK5gbYGSbuPFO1XiXOPCw+p/RQqDi4ydvmVKUtjJD1WoGi26zOZ4qXaRfi7y4DzJi3KVe44mLCVlcXUAd3bLvcbu3Am09TaFz5ZMrBEnLqOpxe42adzzjxH5ke6sP914SGi7oaBxvA/UKjzTGNptZQbuQPPSOPnufUqTk1bVWYPygu8otB9S0+ySMvB3H0idpMlafGROgcRMwZc6OMu+o6qL2OyltF7S6C/U6o87+LYX6Ax5iVp83qQwmYDQJPlf1O/qeizjHupMc59nVDMflZwHt8yjJPhHRsIuTLHtTiRWApaoa4+P/ABF4PSLrzbtFW1v7prIa2zWi0HmQLF3M87DZanCVi5tTEPPMM6xu73LWj1Ubs7khrONUjc7cN0Rm0tlVBGXo9lKpbq0yd4J3HMffJabLML3dIamSBz+Jvv8AZXoNLL2hoEKnzLLyCSzimU36K4rwo8B3bjFiIgHkB+F3Lz5J6vDdTIEOBNwIPXbe8eXPjWPJa/U3wOBuLwSDsRwmVz8xDneIaXcWT8Q5sP4revBNaEcWV9bEmlUl+rSbGNrTeOBH2F6f2YzIVKYvMD/4V5diaonTUjSYg3seB6cP1kbWPZHM3YeuKL9jdl7Oabw3zmR1TxdOxJxtHsLCngoGEramggyCJHkVNYV0I5w0iVItARIURQlAAlCQjKFADZCApwoCgwBclK5ADwRhAEYQAQShIESDRQlSJUAKFxKVNYh1ljAqMS8uOnhJJ905RrBsDidgufSiev03TeEoQS9252H5RwXKrstqhnPap0adug/UrJZNhjLq7pIExxm5HhHGNuU8yDOgzImq7Q0Twd5cfdRc6xdHDUi0EEj4jtHJrR5QAOvOVOW22PHVIzGZ4jutVZ96j5FNu+mBN+em09dPVWfYak+C9/xP0jjZovfqTw/tWRwdGrjcVqfOnVppt4Nay5c5vACQf7i6ORXqeQ0mgwxvw/L+VlVUfsdvVh5uxlKnqqHr1JOwjmeS8/zLEOrPImBx/taef9xv9gLVdqZe7+o7bYbnlYbD5ys0ylqMNG5MDe5tvx6np5KOSXKX9FcUaRIpYQ1GsosEARPk3Ye5cfMhbfKsvbSYGgRAUPJct7tondX1Nll1Yoatk8k/EM1RZQa1OVZPYoz2onEyMjBdscuMd9Ts5vxRxbxtsf4WWGYU3tioLiLjn5Hj09l6jmlEFpXmGeZZ3TyQPAZi239p5j6Ka+mV72iacMK1ItYZIEtJuBwLXDlfyTWW4LvB3ZEOZ8PMOm4E7cL3Hms4yq+k/VScWncBxJaf8XTb5rSMr69NQPDao8QJjS6PiaZ4ETccpvCba0Ta9PQuyeYlzdFSzxY7/F689/PzC1dIrF5PWbVAeBpfEG9+cOPEixH8rX4WpIE78fPiujFK0cuRUyUuXArlYQRIlSFAAlCUZQlAAFAU4UBQYAuSpEAPNRhC1EEAEEqQJUGihEEiVACpmqPv1TyZxGyxgRKokkeX38lDq4mfC3l7eaOrX0uqTyH6qLTH9Mni76LllL6/JZIpsfjSwENsPxOiTM/O+w8uay9VtTF1e7pi3F14Y2Tx/Mb3nfbiVd9oqbmNaxolz4AF4ksbuR+G0FSezmGaGxuybmL1XCxMfl+W/mYw72WfWiRlWVUqDC2kPwkaucTOj+2Sb2m55KywOILaXghrfzc+o/dTsPS1PB+/uwWbzmliBWfSJDaA0mmGWJbu4vPF0+EN2tPG1EtuQl38SszA63FrQSCZJm7jtJcen7W43WS5YGw4gTFuQ8vksziKWLmWMIbwu30HlxniY4WbdZNjMS1wFZvh2mRbrHJSUUnbOjuNI2FBoCOtiGsEuMBN0nyLKuzsamaSCZ9N1086Wjn4W9lLnfa54caeGpy7mQbHy++PSavD5XjsSNWIrvpg8zpPo2nBHqfQqTmWBxRDKeF0sfUeG6tIOlu9R8HcNBFzMlzRxkY3HZPmDsT3DsTi+MkVNDSZbpa0NdufHPhAAAMkGzwjKaszJkjB0bml2dZTFsViJ/8AJLf+jpCHE5cKjC03/dZCj2bzCniRRZjqtShPiOqagby1OnSeEjZenYXCBjAAIgR7KOSJWM2ecjs0Ayo/g0uLhwFgJjyJVLhapYS07GSL3A3sefUdFts3xfdsr0iCdTpj8zS1vh8iZb6rLPpd7XcALNhjTBAcWBzXO/5ODvSFNbH7Cy3PP9u8VC6aTjpLrnS8GDqH4WyfQm1iJ9ZybHNqsDmkEG9vqF4Dn+Kdg8TpAL6bmNLm8PFMgA2kX9yNiVtewmZPp1GNpkuovjTuWjmI3aROyuvi7OecbtHr7HJxR6bk+0rpOcUoUSEoAQoSiKQoAAoCjKEoMAKRKVyAHmowgajCAFCVIEqDQglSBKgBUxiPonkxiTA+/VLLoF2UOPMl56sHzITmCGqOQaPnBPyTWYO00i7i46h5cPr805gjpknYj5mf00rij+46X+0DO8M14Bi7QRPLgfkVQUarjVpsbIaXRA5NvHl/Cue9LnVG82ah56hP6qDg60VGtECC3Va5bqg35WPuEknylY8dI0WWuID9VoMD/GLffRZTtnjXim6owFxp3LWxLm8Ynbn6FW2YYhzNQG8gecS2/TcqnfWD9L92usfI8/mCmyzpJGY47swmZ0szexlTvakVGSxlFwYA4h0NL7aoOgnjEwvQOy2S16dNuutUq6rlr5cWXMeI7iIkEyPkuyTKtJ3eQDIl508dmz1+S19BkBdClGcaSFcZRlbYzhmaHaeH0SZgwFwnZHWN5Q4y7ZSN0mhkraYb6NN0OjxAQLkACZO3X6BQ8Xl3eCDP/Y/fBP4WrKntVIvkhGnEqcNgG09gJR1nWUvFOVZWcpTdOkWgrVspcyw1IvBqbG3zkfP6pnE9mg2KtJwc0mYuIMWBP/xMdpsUGs1OIDQQDNrEx+qOvndLDYU1DUEBvhbI8Rjwtb1NtkkUro18u0efducucaz5afCKYaSInS2XEdPGfZP9jKVWk4taYvbV8MiSD0tI9Vo8hzUY+j3ddrWvcCWPb+F8TsZtsPux0MDHDSQYPKQSLJ3J1RPvZuckzbvAG1AWv5G4MbwRur1hWYyaiyzovvfh0WjpOXTBto5ZKmPriuBXJzASkKIoSgASgKMoHIMAK5KkQA61GEDUYQAQSpAlQaEEqFEsA5QcwqD4ffyUuq+BKqjTc8yLDif0HNSyy/ih4L1kHGUTWOk7GLcmi/8AHquzcFopxtrBPsR+vyVq6k1jbevMnqq7MAHscJuDvysP3UJx4p32VjK3+CtoYgNrgnY2P/LUmH0iKvMTHly/n+FBxTyXw2ZOwG8Cw8tz7q4wj9Lqki5DXe4/cH3XPB62WkqG8a8knmPrZUmBdaoPw96QPPVw6furfMKjWhxe6L3P36LK1O0VIVG0QI30D/EAuvxd4p90sk5JpFMdJ7N7lThZXgNllsjrTBWkY9dOGWhM8fkMYrgE7pGm5Vfn+Eq1Gf0XaX3E2tPETaVTYXLMy0w+rTN9yCbdWggH5Jm6b0Yopx7J9TFsa+GuBcIkAiYPMK+1Kiyns1Rw9xLnF2t7ju9/5nHirlzlsdCzafQ3XKqcU5T61RQKgkpHtjJ0jEf6j2wbjvNSkI5gvEj1C85xVIN7pwgw1ovF4A0m+xAMR5r0X/VTEtZh6TD+OuweUAmfospkuV98wMJDWtOovd8LBqhwIG5kEgbkkDiFaGkSltmo7G0202GqRANmWjfxGPKw91budcCSfC254mx/U+ygPfTqRRpS1tKNIO7wfzHnckjmEuExc13U4+E7/wBo2d5GyjJDo1uXvj781oMO6yzFB97cT9IWgwT7LpxvRzT7LJpRJthRqohxQlKkKABKByMoHIMAK5cuQA6EYTYKMIAIIkIRBYaKuJXBN1TwQwAcNZjhxTlRsCyWmLJqrV4JOlY3ZFxBJPkqrFUuBmJki1z1Vo+pvHuqzFPbsDfouTLsvAZoUQD/ABJ9YUermLGlxbBixcbkxvHAASq7OccKZLJ1ECXifCwdY3Pv7rMZziy6nJM0yYIbe55gb9R9bzDk3ouoekHth2lLgRTPhB9xzPncjoGn8QC89x2NqNqMeCSWHckmXTqcfXVHkAFcZp4g6bXl0mbAiTHP5R5BNU8v1M16Zgk/9QAdt7An0XZiSiiORNnrnY7HB7GOb8LmhzfI8PRbanVXlfYbMKTQymDAddgkWdckD2JjzXo1CpIiVFri6LcuSssH1wN1X1s/Y21j9UxmuWd6J1vEbhjtM/JVdLA5dTH9RgHPvC5xP+WuZW8n9lsWODVtWP4jtYzVEku/Ixpe4/8AECU5hs6rPdp/29Ro4ucAwD0cZPpKGlm+Ha3u8LTn/BkNHrspuEpuPidv9OiG/wAjTUEuq/0kSo7zCfq2CzfafN+4ove2NYY4tB5gTJHIWQjlPMf9Vs37/FtoMNqIvy7x13SeQAHzTmXY5rmMY13hjUZkFzyILj0gtgcPVYc1XO1SZe9xLidzJn5m58gtFkrDpHC9jeRF59JC6JpKJDHbk2bTAveHgm+/ASQNRbeOoV4aIAaQz+pUhrj+RgGqPU3VZkzi0tLhPPjFjB91dYalU0Om4Ozt5k8T97LkcjoocoOdrBbz2W0wDpAWMp1gHBvIX81rcqNlf/nv058xbsTibYjXUQOKEpSkKABKFxRFA5aYCuXJEAOBGE2EYQAYRBAiWGhSgA/dKV3NACuMD0VZJJH9x+Q+5U3FvhqjUx4h0b9T+ylPbSHjpWDiIFlTYmpoa5ws64afyk8fvkrbGH6qmr1b2Ew0nnJiQB6gewXNndMtjKbDZHrBdWfpEAlp3ncF7tyenXgs/m9GgDpDi+JPhhoAHHiT6zutTqaToqPhu7nT4S7czzH3yWb7RV6YtScI2HdgzvfxHh6rlVKjqVsw+bBrdmlmqw1k6jM+sWI2n2MX3Z6iw4Qh4uX6P/USR6FQcv7PmrV1OEMAPigGAbnf8TjboFcYhzWN0Mpy25lxAEm3muznGKpEHFydszmCwtWpS1MguDxpLbOMGWmR+KD8ls+zvbAazQxJ01Gu0ySNL4tvwd0WQp55/tgX0GtkTAjwsJ/FpnYRbyCqn1mPJcfxEkzxJ3LT16qkvmrFj8XR9C4bEBwBBUpuDpuu4A+a8W7NdoMRRLWNdrafhD5vyAd+E22ut9l/bjD/AA1ppO5PsJ/y2PuorT2WcW1o2Yw9MbAJquAAqR/arCRPfs/7BUOc9t6UaaMvPSw9/wBpTylHwSMJelxnOaNptJPtzXnOc4p9WhiKjtyxwHICDYKVWxT6t3GSfYeSdxeB/wDzuZ+YEf8AqVKyvGkeXYDBmoRp34rW5E3ZpG2xP69ZUDIcIWAE7larB02ggxv9eB81TLO9E4RpFzgKLmiOYv8AwptKq6n8JIPQ38kFKpYRuVZYHKy67lOEG3Zk5pA4Cial3NAM7gRPmFq8vpQITGEwUBWdJkLshGjknKx5iNCESoKIUJSlCUAIUBREoCtMEXISlQAYRhNhGCgBwJQgCILDQkqFKgBjE7O6D+f2QMIn0j2KeHHzP7foo7LOI++SRrYy6I+YGRHE/QffzVa1rdcvs0WA/MT9/JWVc+P0I+kquxrIc0+fvYj6LlzLfItj+hnNcOHXHo0be6yGbU3AEiIHCB7CVrq1QtpAjdwd8W25F48lkWvccRT78AtlvDw6i4RPu35ny4pxuWjqxypDuNqFlIMFnBoJ1WAc5sgHoBc/yqGrhnNaCXHbU57jeXQZHIxA6cNlqO09AGuBfSdZdyI16APZnzUTFZcamhsW+Nw/C4/ha7oDw6DgrpU6Fu1Zg8yy5pIfL9pJLQS8HZ2kG09d480xgctbUf3ZBAMb7m/PYEytTm2BLiY2aJnYn+4u4C1mgbDeIVPl2Eio0NafETMWgFpaC50yTc7/AKK6yaE4ejOFpTWII0sMAQDaNiCTYi11s8JgGYhpZVAL2iCYHi/K4efyMrP5lhA1z3wYB0gCBIbFm9S4knyWnyeu06HtnULOBEEzvAI8lKbvZaPRSYnscWk6dl1Ds64br0hlJrmgi8pt2DHJbTM/UMtluUQZKLMWeIN5ArWMwkBZnEN1VH9LLGjFKzFDCODyyOJg9FZsZLSJiLA9VoW5c124UfE9mn6NNJ1pkzvve6Lszos+zuE1gOcFsMPhwAqjs5RDGhrrEc1pWMXZiS4nHkb5AMppwBFpSqpMRcuKQoAQoSlKElAAlCSlcUBK0wRKglcgwcBRApsFECg0dBRgpoFGCgA5SyglKsNOb9+qjubLinnvACZA578uEpWCIgMuM8/fe46IMVR1C/vyPAp3F1AOpUJ9R54qE5Jaey8It7RFzaj/AEBJAIJEkwN3ETymVnhlVWNRr0uElr3AAAyLagL/AHMrQ1DpaWvuH8hOmNnRxM/RZ3F9nq9QQ52pvnM8fhtH3uuKStnVDWmx/F4hrhLajXloHwnhznjPT5qJiMYGvk7Gm0neI8Wr30n5JzK+z3cE66ljNnETfeen7KvzXCmq52iQWyG9RF/Ii5jkTulSo3RZYKrSrMdqIbTbJeTbU6+o+VoHKJ5KgzHM6DCO6pTBEF0T08OzOB4m94TGXhwoCm4Q4PHeDhsCD1GoA9VoMH2WpVIJcWnh8Jnr4h6+qrGvTHoqc4rCrTadNpa8xezhDr8w6OP4gjwOasAAqENBmA4x7P8A35bLQjs26lID9U3EgR1mBsVAqZMQ4mCN5At9+dwUP6Gi0XuV45hHhd5jY+cK6peK6xNHB6HAhsRysSIWtyisCqY5bonkjq0WL2eErJ4Sjqe939zvqtfX2Pks9llPwE/3O+pVJrZOL0BTpQVPohM1I9UNOqRsCp0kNtk9zAmHYgj4XH0KQlzviMDkP3QB7BYJmYOtxlfg4+sJ1uZ1hvB9FwFkzUatuS9FqL8JAzt3Fg90/SzqmfiBb8wql9NRqjVqyTQfpwZrWVA4S0gjouJWOoY99F0tNuI4FanCYttRoc3Yq+PIpEJwcR5xTZKVxQEqpM6UiAlKgwMFGCmGuRgoAeBRApoFECg0eBXSmwUUoA7fdNY2uGNnidk9KoMXie8fbYWH7+v7KWSXFFMceTDbJublPMpIKAU6kxc0YWdMpURXYYSmK2D5GFcFgUSuU8sSEWRmeqZdTDpcxp6kTf1UmrSYWgadtiLGBsDz+/JFi3JltRR4pFHK+yLUwVOCdIcDIJHM3Mjdpm/r1TuGrMYC0ut1/VBjsvL/AB0nupviNTdyOAdzHmqipha48NfxDbUwBrh1jY+XFScWuh1TGc6xT9X9Kq6J2aXEeolSOztZwGmo7UHCRMm83ieCpP8AZE1PiMC07SOH62PLyV1QZ4qcbaBI9T+yyCHk9UXLmh4+fryR4RxY4JvCgyfP9ApTaasoici7bVDm+ircHT0tjqfmU7SMBcDCr2SWgTSbMwJ58UFR4CStUUCuSUdG9jeMx52bcqTlWEPx1Nzw5IMBggDqO6sjZCXrFcvEOlIWpoVF3epkIznsUatTUnUmazrIaQJlLjU52azHS80ybG481HzN6osPitFVruqWOpDyXKJ6lrlA4qJga+poT7nLsOMUuXJouSoMFa9ONcuXIAMORhy5cg0IFEHJVyAIuZ19FJxG+w9bKhwxXLly538kdWFfEs6BU+m5cuWwMmc+ootdy5cmkYinxj0w1y5coMqiyw5snKjRF9oPyC5ch9GGXzSkKYtu4z5WP36JMnvE8BHsuXKSVMr4XVID3/iP0UumFy5ViIx4FA9y5cnFQy9NEJFyU0fovhOveuXJk9CtEV9VCyukXJGbRIa9M1nrlyexShzR1iszXfBlcuSjo33ZzEaqbfIK4c9cuXYjiYyXrly5aKf/2Q==";

            var imageName = _imageWorker.SaveImage(base64);

            return new string[] { "value1", "value2" };
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public ActionResult<string> Get(int id)
        {
            return "value";
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
