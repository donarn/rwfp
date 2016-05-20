// Learn more about F# at http://fsharp.org
// See the 'F# Tutorial' project for more help.

//[<EntryPoint>]
//let main argv = 
//    printfn "%A" argv
//    0 // return an integer exit code

open System

let convertDataRow(csvLine:string) =
    let cells = List.ofSeq(csvLine.Split(','))
    match cells with
    | title::number::_ ->
        let parsedNumber = Int32.Parse(number)
        (title, parsedNumber)
    | _ -> failwith "Incorrect data format!"

// convertDataRow("testing,1234")

let rec processLines(lines) =
    match lines with
    | [] -> []
    | currentLine::remaining ->
        let parsedLine = convertDataRow(currentLine)
        let parsedRest = processLines(remaining)
        parsedLine :: parsedRest

// let testData = processLines ["Test1,123"; "Test2,456"]

let rec calculateSum(rows) =
    match rows with
    | [] -> 0
    | (_, value)::tail ->
        let remainingSum = calculateSum(tail)
        value + remainingSum

//open System.IO
//
//let lines = List.ofSeq(File.ReadAllLines(@"c:\temp4\rwfp_ch03_data.csv"))
//let data = processLines(lines)
//let sum = float(calculateSum(data))
//
//for (title, value) in data do
//    let percentage = int((float(value)) / sum * 100.0)
//    Console.WriteLine("{0,-18} - {1,8} ({2}%)", title, value, percentage)

open System
open System.Drawing
open System.Windows.Forms

let mainForm = new Form(Width = 620, Height = 450, Text = "Pie Chart")

let menu = new ToolStrip()
let btnOpen = new ToolStripButton("Open")
let btnSave = new ToolStripButton("Save", Enabled = false)
ignore(menu.Items.Add(btnOpen))
ignore(menu.Items.Add(btnSave))

let boxChart =
    new PictureBox
        (BackColor = Color.White, Dock = DockStyle.Fill, SizeMode = PictureBoxSizeMode.CenterImage)

mainForm.Controls.Add(menu)
mainForm.Controls.Add(boxChart)

let rnd = new Random()

let randomBrush() =
    let r, g, b = rnd.Next(256), rnd.Next(256), rnd.Next(256)
    new SolidBrush(Color.FromArgb(r,g,b))

let drawPieSegment(gr:Graphics, title, startAngle, occupiedAngle) =
    let br = randomBrush()
    gr.FillPie(br, 170, 70, 260, 260, startAngle, occupiedAngle)
    br.Dispose()




[<STAThread>]
do
    Application.Run(mainForm)


